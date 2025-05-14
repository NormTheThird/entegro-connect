namespace EntegroConnect.API.Filters;

public class ApiKeyFilter : IAuthorizationFilter
{
    private readonly IConfiguration _configuration;

    public ApiKeyFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Query.TryGetValue("api-key", out var extractedApiKey))
        {
            context.Result = new UnauthorizedObjectResult("API key was not provided.");
            return;
        }

        var apiKey = _configuration.GetValue<string>("ApiKey");
        if(apiKey is null)
        {
            context.Result = new UnauthorizedObjectResult("API key is not set");
            return;
        }

        if (!apiKey.Equals(extractedApiKey))
        {
            context.Result = new UnauthorizedObjectResult("Invalid API key");
            return;
        }
    }
}