namespace EntegroConnect.API.Controllers;

public abstract class BaseController<T> : Controller
{
    readonly internal IConfiguration _config;
    readonly internal ILogger<T> _logger;
    readonly internal IMessageService _messageService;

    public BaseController(IConfiguration config, ILogger<T> logger, IMessageService messageService)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
    }

    internal IActionResult ExceptionResult(Exception ex, MethodBase? methodBase)
    {
        var response = new BaseResponse { ErrorMessage = ex.Message };
        var name = methodBase?.ReflectedType?.FullName ?? "Unknown";
        _logger.LogError($"{name} Exception: {ex}");
        _logger.LogError(name + " Exception: {@exception}", ex);

        if (ex is KeyNotFoundException)
            return NotFound(response);
        else if (ex is DuplicateNameException)
            return Conflict(response);
        else
            return BadRequest(response);
    }
}