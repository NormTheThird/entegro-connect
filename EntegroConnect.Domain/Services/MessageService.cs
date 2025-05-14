namespace EntegroConnect.Domain.Services;

public class MessageService : IMessageService
{
    private readonly IConfiguration _config;

    public MessageService(IConfiguration configuration)
    {
        _config = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task SendEmailAsync(string body, string subject)
    {
        try
        {
            var message = new MailMessage();
            message.From = new MailAddress("entegro-connect@entegrohealth.com", "Entegro Connect");

            message.Subject = subject;
            message.Body = body;

            var exceptionEmailString = _config["EmailList"];
            var toList = exceptionEmailString?.Split(',').ToList() ?? new();
            foreach (var to in toList)
                message.To.Add(to);

            var client = CreateSmtpClient();
            await client.SendMailAsync(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"SendEmailAsync Error: {ex.Message}");
        }
    }

    public async Task SendExceptionEmailAsync(Exception exception, string subject)
    {
        try
        {
            var message = new MailMessage();
            message.From = new MailAddress("entegro-connect@entegrohealth.com", "Entegro Connect");

            message.Subject = subject;
            message.Body = GetExceptionDetails(exception);

            var exceptionEmailString = _config["EmailList"];
            var toList = exceptionEmailString?.Split(',').ToList() ?? new();
            foreach (var to in toList)
                message.To.Add(to);

            var client = CreateSmtpClient();
            await client.SendMailAsync(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"SendExceptionEmailAsync Error: {ex.Message}");
        }
    }



    private SmtpClient CreateSmtpClient()
    {
        var client = new SmtpClient(_config["SmtpServer"], Convert.ToInt32(_config["SmtpPort"]));
        client.EnableSsl = true;
        client.Credentials = new NetworkCredential(_config["SmtpUsername"], _config["SmtpPassword"]);
        return client;
    }

    private string GetExceptionDetails(Exception exception)
    {
        string exceptionDetails = "";

        if (exception != null)
        {
            exceptionDetails += "Exception Type: " + exception.GetType().FullName + Environment.NewLine;
            exceptionDetails += "Message: " + exception.Message + Environment.NewLine;
            exceptionDetails += "Stack Trace: " + exception.StackTrace + Environment.NewLine;

            if (exception.InnerException != null)
            {
                exceptionDetails += "Inner Exception: " + Environment.NewLine;
                exceptionDetails += GetExceptionDetails(exception.InnerException);
            }
        }

        return exceptionDetails;
    }
}