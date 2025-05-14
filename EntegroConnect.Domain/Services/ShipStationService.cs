namespace EntegroConnect.Domain.Services;

/// <summary>
///     ShipStation Service
///     
///     https://www.shipstation.com/docs/api/
/// </summary>
public class ShipStationService : BaseService, IShipStationService
{
    public ShipStationService(IHttpClientFactory httpClientFactory)
        : base(httpClientFactory, "ShipStationClient") { }

    public async Task<ShipStationShipmentsModel> GetShipmentByUrlAsync(string url)
    {
        var retval = await GetAsync(url);      
        return retval?.ToObject<ShipStationShipmentsModel>() ?? new();
    }
}