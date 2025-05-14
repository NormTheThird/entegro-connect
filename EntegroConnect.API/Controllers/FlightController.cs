namespace EntegroConnect.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlightController : BaseController<FlightController>
{
    public FlightController(IConfiguration configuration, ILogger<FlightController> logger, IMessageService messageService)
        : base(configuration, logger, messageService) { }
}