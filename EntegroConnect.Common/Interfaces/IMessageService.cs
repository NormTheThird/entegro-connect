namespace EntegroConnect.Common.Interfaces;

public interface IMessageService
{
    Task SendEmailAsync(string body, string subject);
    Task SendExceptionEmailAsync(Exception ex, string subject);
}