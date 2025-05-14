namespace EntegroConnect.API.Controllers;

[Route("api/[controller]")]
public class StatusController : BaseController<StatusController>
{
    public StatusController(IConfiguration configuration, ILogger<StatusController> logger, IMessageService messageService) 
        : base(configuration, logger, messageService)  { }

    /// <summary>
    ///     Gets the status of the API
    /// </summary>
    /// <returns></returns>
    [HttpGet("status")]
    public async Task<IActionResult> GetStatus()
    {
        try
        {
            var response = new BaseDataResponse
            {
                Success = true,
                Data = new
                {
                    Environment = _config.GetValue<string>("Environment"),
                    NarVarUrl = _config.GetValue<string>("NarVarClientUrl"),
                    EmailList = _config.GetValue<string>("EmailList")
                }
            };
            return Ok(response);
        }
        catch (Exception ex)
        {
            await _messageService.SendExceptionEmailAsync(ex, "Create NarVar Order Exception");
            return ExceptionResult(ex, MethodBase.GetCurrentMethod());
        }
    }

    [HttpGet("test-email")]
    public async Task<IActionResult> TestEmail()
    {
        try
        {
            var response = new BaseResponse();

            await _messageService.SendEmailAsync("This is a test email from the Entegro Connect API", "Test Email");

            response.Success = true;
            return Ok(response);
        }
        catch (Exception ex)
        {
            await _messageService.SendExceptionEmailAsync(ex, "Create NarVar Order Exception");
            return ExceptionResult(ex, MethodBase.GetCurrentMethod());
        }
    }
}