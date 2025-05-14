namespace EntegroConnect.Common.Interfaces;

public interface ISendGridApiService
{
    Task<HttpStatusCode> SendExceptionEmailAsync(string body, string subject);
}