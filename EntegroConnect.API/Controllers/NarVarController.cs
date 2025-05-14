namespace EntegroConnect.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NarVarController : BaseController<NarVarController>
{
    private readonly INarVarService _narVarService;

    public NarVarController(IConfiguration configuration, ILogger<NarVarController> logger, IMessageService messageService,
                            INarVarService narVarService)
        : base(configuration, logger, messageService)
    {
        _narVarService = narVarService ?? throw new ArgumentNullException(nameof(narVarService));
    }

    /// <summary>
    ///  Get NarVar order by order number
    /// </summary>
    /// <param name="orderNumber">The order number of the order</param>
    /// <returns>NarVarOrderModel</returns>
    [HttpGet("orders/{orderNumber}")]
    public async Task<IActionResult> GetOrder(int orderNumber)
    {
        try
        {
            var response = new BaseDataResponse();

            var garageAccountSettings = await _narVarService.GetOrderAsync(orderNumber);

            response.Data = garageAccountSettings;
            response.Success = true;
            return Ok(response);
        }
        catch (Exception ex)
        {
            return ExceptionResult(ex, MethodBase.GetCurrentMethod());
        }
    }

    /// <summary>
    ///  Create a new NarVar order
    /// </summary>
    /// <param name="orderModel">The order model to create</param>
    /// <returns></returns>
    [HttpPost("orders")]
    public async Task<IActionResult> CreateOrder(NarVarOrderModel orderModel)
    {
        try
        {
            var response = new BaseResponse();

            await _narVarService.CreateOrderAsync(GetTestData());

            response.Success = true;
            return Ok(response);
        }
        catch (Exception ex)
        {
            return ExceptionResult(ex, MethodBase.GetCurrentMethod());
        }
    }




    private static NarVarOrderModel GetTestData()
    {
        return new NarVarOrderModel
        {
            OrderInfo = new NarVarOrderInfoModel
            {
                OrderNumber = new Random().Next(1000, 9999).ToString(),
                OrderDate = DateTime.Now.ToString("YYYY-MM-DDThh:mm:ssTZD"),
                CheckoutLocale = "en_US",
                OrderItems = new List<NarVarItemModel>
                {
                    new NarVarItemModel
                    {
                        Name = "Test",
                        Sku = "123456",
                        Quantity = 1,
                        ItemImage = "https://entegrohealth.com/products/30-47100/large/7363808.jpg",
                        ItemUrl = "https://entegrohealth.com/events/161975/products/220900",
                        FulfillmentStatus = "SHIPPED",
                        UnitPrice = 15.00m
                    }
                },
                Shipments = new List<NarVarShipmentModel>
                {
                    new NarVarShipmentModel
                    {
                        Carrier = "dhl",
                        TrackingNumber = "1234567890",
                       // ShipSource = "DC",
                        ShipDate = DateTime.Now.ToString("YYYY-MM-DDThh:mm:ssTZD"),
                        ItemsInfo = new List<NarVarItemInfoModel>
                        {
                            new NarVarItemInfoModel
                            {
                                Quantity = 1,
                                Sku = "123456"
                            }
                        },
                        ShippedTo =
                            new NarVarCustomerModel
                            {
                                FirstName = "Trey",
                                LastName = "Norman",
                                Phone = "555-555-5555",
                                Address = new NarVarAddressModel
                                {
                                    Street1 = "123 Main St",
                                    Street2 = "",
                                    City = "No Where",
                                    State = "TX",
                                    Country = "US",
                                    Zip = "77777"
                                }
                            }

                    }
                },
                Billing = new NarVarBillingModel
                {
                    BilledTo = new NarVarCustomerModel
                    {
                        FirstName = "Trey",
                        LastName = "Norman",
                        Email = "trey.norman@email.com",
                        Phone = "555-555-5555",
                        Address = new NarVarAddressModel
                        {
                            Street1 = "123 Main St",
                            Street2 = "",
                            City = "No Where",
                            State = "TX",
                            Country = "US",
                            Zip = "77777"
                        }
                    }
                },
                Customer = new NarVarCustomerModel
                {
                    FirstName = "Trey",
                    LastName = "Norman",
                    Email = "trey.norman@email.com",
                    Phone = "555-555-5555",
                    Address = new NarVarAddressModel
                    {
                        Street1 = "123 Main St",
                        Street2 = "",
                        City = "No Where",
                        State = "TX",
                        Country = "US",
                        Zip = "77777"
                    }
                }
            },
        };
    }
}