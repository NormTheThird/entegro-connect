namespace EntegroConnect.Domain.Services;

public class FlightService : BaseService, IFlightService
{
    public FlightService(IHttpClientFactory httpClientFactory) 
        : base(httpClientFactory, "") { }
}