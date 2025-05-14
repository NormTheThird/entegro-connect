namespace EntegroConnect.API.Controllers;

[ApiController]
[ServiceFilter(typeof(ApiKeyFilter))]
[Route("api/[controller]")]
public class ShipStationController : BaseController<ShipStationController>
{
    private readonly INarVarService _narVarService;
    private readonly IShipStationService _shipStationService;

    public ShipStationController(IConfiguration configuration, ILogger<ShipStationController> logger, IMessageService messageService,
                                 INarVarService narVarService, IShipStationService shipStationService)
           : base(configuration, logger, messageService)
    {
        _narVarService = narVarService ?? throw new ArgumentNullException(nameof(shipStationService));
        _shipStationService = shipStationService ?? throw new ArgumentNullException(nameof(shipStationService));
    }

    /// <summary>
    ///     Order Shipped: This is called from the ShipStation webhook when an order is shipped.
    /// </summary>
    /// <returns></returns>
    [HttpPost("order-shipped")]
    public async Task<IActionResult> OrderShipped()
    {
        try
        {
            var response = new BaseDataResponse();

            using var reader = new StreamReader(Request.Body);
            string requestBody = await reader.ReadToEndAsync();
            _logger.LogInformation($"Request Body: {requestBody}");

            var jsonObject = JObject.Parse(requestBody);
            var resourceUrl = jsonObject["resource_url"]?.Value<string>() ?? string.Empty;
            if (string.IsNullOrEmpty(resourceUrl))
                throw new ArgumentNullException("resource_url is null or empty");

            _ = ProcessOrders(resourceUrl);

            response.Success = true;
            return Ok(response);
        }
        catch (Exception ex)
        {
            return ExceptionResult(ex, MethodBase.GetCurrentMethod());
        }
    }



    private async Task ProcessOrders(string resourceUrl)
    {
        resourceUrl = resourceUrl.ToLower().Replace("includeshipmentitems=false", "includeShipmentItems=true");
        var shipments = await _shipStationService.GetShipmentByUrlAsync(resourceUrl);
        _logger.LogInformation("Shipments: Total Shipment Count - {count} | Current Page - {page} | Total Pages {pages}",
            shipments.TotalShipmentCount, shipments.CurrentPage, shipments.TotalNumberOfPages);

        var body = $"Total Shipment Count - {shipments.TotalShipmentCount}\n\n";
        foreach (var shipment in shipments.Shipments)
        {
            await CreateNarVarOrder(shipment);
            body += $"Order Number: {shipment.OrderNumber} | Carrier Code: {shipment.CarrierCode} | Tracking Number: {shipment.TrackingNumber}\n";
        }

        await _messageService.SendEmailAsync(body, "Order Shipped");
    }

    private async Task CreateNarVarOrder(ShipStationShipmentModel shipment)
    {
        try
        {
            _logger.LogInformation("Creating order in narvar for {orderNumber} : {carrierCode}", shipment.OrderNumber, shipment.CarrierCode);
            var narVarOrder = ConvertShipStationShipmentToNarVarOrder(shipment);
            await _narVarService.CreateOrderAsync(narVarOrder);
        }
        catch (Exception ex)
        {
            await _messageService.SendExceptionEmailAsync(ex, "Create NarVar Order Exception");
            _logger.LogError("CreateNarVarOrder Exception Message: {message}", ex.Message);
            _logger.LogError("CreateNarVarOrder Exception: {ex}", ex);
        }
    }

    private NarVarOrderModel ConvertShipStationShipmentToNarVarOrder(ShipStationShipmentModel shippment)
    {
        var addressModel = new NarVarAddressModel
        {
            Street1 = shippment.ShipTo.Street1,
            Street2 = shippment.ShipTo.Street2,
            City = shippment.ShipTo.City,
            State = shippment.ShipTo.State,
            Zip = shippment.ShipTo.PostalCode,
            Country = shippment.ShipTo.Country
        };

        var customerModel = new NarVarCustomerModel
        {
            FirstName = shippment.ShipTo.Name,
            LastName = "",
            Email = shippment.CustomerEmail,
            Phone = shippment.ShipTo.PhoneNumber,
            Address = addressModel
        };

        var itemsInfo = new List<NarVarItemInfoModel>();
        var orderItems = new List<NarVarItemModel>();
        foreach (var item in shippment.ShippedItems)
        {
            itemsInfo.Add(new NarVarItemInfoModel
            {
                Sku = item.Sku,
                Quantity = item.Quantity
            });

            orderItems.Add(new NarVarItemModel
            {
                Sku = item.Sku,
                Name = item.Name,
                Description = item.Name,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                ItemImage = "",
                ItemUrl = "",

                // This flag indicates the item status. It is used for Track, Notify, Return.
                // Accepted enums:
                //     NOT_SHIPPED,
                //     SHIPPED,
                //     CANCELLED,
                //     PROCESSING,
                //     PICKED_UP,
                //     NOT_PICKED_UP,
                //     READY_FOR_PICKUP,
                //     DELAYED, RETURNED
                FulfillmentStatus = "SHIPPED"
            });
        }

        return new NarVarOrderModel
        {
            OrderInfo = new NarVarOrderInfoModel
            {
                OrderNumber = shippment.OrderNumber,
                OrderDate = shippment.CreateDate.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ssZ"),
                CheckoutLocale = "en_US",
                OrderItems = orderItems,
                Shipments = new List<NarVarShipmentModel>
                {
                    new NarVarShipmentModel
                    {
                        Carrier = GetCarrier(shippment.CarrierCode),
                        TrackingNumber = shippment.TrackingNumber,
                        ShipDate = shippment.ShipDate.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ssZ"),
                        ItemsInfo = itemsInfo,
                        ShippedTo = customerModel,
                        ShipTax = 0.0m,
                        ShipTotal = shippment.ShipmentCost
                    }
                },
                Billing = new NarVarBillingModel
                {
                    BilledTo = customerModel
                },
                Customer = customerModel
            }
        };
    }

    private static string GetCarrier(string carrierCode)
    {
        if (carrierCode.Equals("stamps_com", StringComparison.InvariantCultureIgnoreCase))
            return "usps";
        else
            return carrierCode;

    }
}