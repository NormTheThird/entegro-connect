namespace EntegroConnect.Common.Models;

public class NarVarOrderModel
{
    [JsonProperty("order_info")] public NarVarOrderInfoModel OrderInfo { get; set; } = new();
}

public class NarVarOrderInfoModel
{
    [Required][JsonProperty("order_number")] public string OrderNumber { get; set; } = string.Empty;
    [JsonProperty("order_date")] public string OrderDate { get; set; } = string.Empty;
    [JsonProperty("checkout_locale")] public string CheckoutLocale { get; set; } = string.Empty;
    [JsonProperty("order_items")] public List<NarVarItemModel> OrderItems { get; set; } = new();
    [JsonProperty("shipments")] public List<NarVarShipmentModel> Shipments { get; set; } = new();
    [JsonProperty("billing")] public NarVarBillingModel Billing { get; set; } = new();
    [JsonProperty("customer")] public NarVarCustomerModel Customer { get; set; } = new();
}

public class NarVarItemModel : NarVarItemInfoModel
{
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;
    [JsonProperty("description")] public string Description { get; set; } = string.Empty;
    [JsonProperty("unit_price")] public decimal UnitPrice { get; set; } = 0.0m;
    [JsonProperty("item_image")] public string ItemImage { get; set; } = string.Empty;
    [JsonProperty("item_url")] public string ItemUrl { get; set; } = string.Empty;
    [JsonProperty("fulfillment_status")] public string FulfillmentStatus { get; set; } = string.Empty;
}

public class NarVarItemInfoModel
{
    [JsonProperty("sku")] public string Sku { get; set; } = string.Empty;
    [JsonProperty("quantity")] public int Quantity { get; set; } = 0;
}

public class NarVarShipmentModel
{
    [JsonProperty("carrier")] public string Carrier { get; set; } = string.Empty;
    [JsonProperty("tracking_number")] public string TrackingNumber { get; set; } = string.Empty;
    [JsonProperty("ship_date")] public string ShipDate { get; set; } = string.Empty;
    [JsonProperty("items_info")] public List<NarVarItemInfoModel> ItemsInfo { get; set; } = new();
    [JsonProperty("shipped_to")] public NarVarCustomerModel ShippedTo { get; set; } = new();
    [JsonProperty("ship_total")] public decimal ShipTotal { get; set; } = 0.0m;
    [JsonProperty("ship_tax")] public decimal ShipTax { get; set; } = 0.0m;
}

public class NarVarBillingModel
{
    [JsonProperty("billed_to")] public NarVarCustomerModel BilledTo { get; set; } = new();
}

public class NarVarCustomerModel
{
    [JsonProperty("first_name")] public string FirstName { get; set; } = string.Empty;
    [JsonProperty("last_name")] public string LastName { get; set; } = string.Empty;
    [JsonProperty("phone")] public string Phone { get; set; } = string.Empty;
    [JsonProperty("email")] public string Email { get; set; } = string.Empty;
    [JsonProperty("address")] public NarVarAddressModel Address { get; set; } = new();
}

public class NarVarAddressModel
{
    [JsonProperty("street_1")] public string Street1 { get; set; } = string.Empty;
    [JsonProperty("street_2")] public string Street2 { get; set; } = string.Empty;
    [JsonProperty("city")] public string City { get; set; } = string.Empty;
    [JsonProperty("state")] public string State { get; set; } = string.Empty;
    [JsonProperty("zip")] public string Zip { get; set; } = string.Empty;
    [JsonProperty("country")] public string Country { get; set; } = string.Empty;
}