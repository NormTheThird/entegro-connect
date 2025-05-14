namespace EntegroConnect.Common.Models;

public class ShipStationShipmentsModel
{
    [JsonProperty("shipments")] public List<ShipStationShipmentModel> Shipments { get; set; } = new();
    [JsonProperty("total")] public int TotalShipmentCount { get; set; } = 0;
    [JsonProperty("page")] public int CurrentPage { get; set; } = 0;
    [JsonProperty("Pages")] public int TotalNumberOfPages { get; set; } = 0;
}

public class ShipStationShipmentModel
{
    [JsonProperty("shipmentId")] public long ShipmentId { get; set; } = 0;
    [JsonProperty("orderId")] public long OrderId { get; set; } = 0;
    [JsonProperty("warehouseId")] public long WarehouseId { get; set; } = 0;
    [JsonProperty("orderKey")] public string OrderKey { get; set; } = string.Empty;
    [JsonProperty("userId")] public string UserId { get; set; } = string.Empty;
    [JsonProperty("customerEmail")] public string CustomerEmail { get; set; } = string.Empty;
    [JsonProperty("orderNumber")] public string OrderNumber { get; set; } = string.Empty;
    [JsonProperty("trackingNumber")] public string TrackingNumber { get; set; } = string.Empty;
    [JsonProperty("batchNumber")] public string BatchNumber { get; set; } = string.Empty;
    [JsonProperty("carrierCode")] public string CarrierCode { get; set; } = string.Empty;
    [JsonProperty("serviceCode")] public string ServiceCode { get; set; } = string.Empty;
    [JsonProperty("packageCode")] public string PackageCode { get; set; } = string.Empty;
    [JsonProperty("confirmation")] public string Confirmation { get; set; } = string.Empty;
    [JsonProperty("notifyErrorMessage")] public string NotifyErrorMessage { get; set; } = string.Empty;
    [JsonProperty("shipmentCost")] public decimal ShipmentCost { get; set; } = 0.0m;
    [JsonProperty("insuranceCost")] public decimal InsuranceCost { get; set; } = 0.0m;
    [JsonProperty("isReturnLabel")] public bool IsReturnLabel { get; set; } = false;
    [JsonProperty("marketplaceNotified")] public bool MarketplaceNotified { get; set; } = false;
    [JsonProperty("voided")] public bool Voided { get; set; } = false;
    [JsonProperty("createDate")] public DateTime CreateDate { get; set; } = new();
    [JsonProperty("shipDate")] public DateTime ShipDate { get; set; } = new();
    [JsonProperty("voidDate")] public DateTime? VoidDate { get; set; } = null;
    [JsonProperty("shipTo")] public ShipStationAddressModel ShipTo { get; set; } = new();
    [JsonProperty("shipmentItems")] public List<ShipStationShippedItemModel> ShippedItems { get; set; } = new();
}

public class ShipStationAddressModel
{
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;
    [JsonProperty("company")] public string Company { get; set; } = string.Empty;
    [JsonProperty("street1")] public string Street1 { get; set; } = string.Empty;
    [JsonProperty("street2")] public string Street2 { get; set; } = string.Empty;
    [JsonProperty("street3")] public string Street3 { get; set; } = string.Empty;
    [JsonProperty("city")] public string City { get; set; } = string.Empty;
    [JsonProperty("state")] public string State { get; set; } = string.Empty;
    [JsonProperty("postalCode")] public string PostalCode { get; set; } = string.Empty;
    [JsonProperty("country")] public string Country { get; set; } = string.Empty;
    [JsonProperty("phone")] public string PhoneNumber { get; set; } = string.Empty;
}

public class ShipStationShippedItemModel
{
    [JsonProperty("orderItemId")] public long OrderItemId { get; set; } = 0;
    [JsonProperty("productId")] public long ProductId { get; set; } = 0;
    [JsonProperty("sku")] public string Sku { get; set; } = string.Empty;
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;
    [JsonProperty("quantity")] public int Quantity { get; set; } = 0;
    [JsonProperty("unitPrice")] public decimal UnitPrice { get; set; } = 0.0m;
}