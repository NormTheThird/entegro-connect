namespace EntegroConnect.Domain.Services;

/// <summary>
///     NarVar Service
///  
///     https://developer.narvar.com/docs/orderapi/api
/// </summary>
public class NarVarService : BaseService, INarVarService
{
    public NarVarService(IHttpClientFactory httpClientFactory)
        : base(httpClientFactory, "NarVarClient") { }

    public async Task<NarVarOrderModel> GetOrderAsync(int orderNumber)
    {
        var retval = await GetAsync($"orders/{orderNumber}");
        return retval?.ToObject<NarVarOrderModel>() ?? new();
    }

    public async Task CreateOrderAsync(NarVarOrderModel orderModel)
    {
        var retval = await PostAsync($"orders", orderModel);
        var response = retval.ToObject<NarVarApiResponse>() 
            ?? throw new InvalidDataException("Null response from NarVar API");

        if (response.Status.IsNotNullOrEmpty() && !response.Status.Equals("SUCCESS", StringComparison.InvariantCultureIgnoreCase))
        {
            var message = string.Join(Environment.NewLine, response.Messages.Select(message => $"Code: {message.Code}, Message: {message.Message}"));
            throw new InvalidDataException($"Status: {response.Status}, Message: {message}");
        }
    }
}