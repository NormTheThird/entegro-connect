namespace EntegroConnect.Common.Interfaces;

public interface IShipStationService
{
    Task<ShipStationShipmentsModel> GetShipmentByUrlAsync(string url);
}