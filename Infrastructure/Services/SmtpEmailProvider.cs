using Application.Services.Interfaces;
using Infrastructure.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;


namespace Infrastructure.Services;

public class SmtpEmailProvider : IEmailProvider
{
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<SmtpEmailProvider> _logger;

    public SmtpEmailProvider(IOptions<EmailSettings> emailSettings, ILogger<SmtpEmailProvider> logger)
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;
    }

    public async Task SendAsync(string to, string subject, string body, bool isHtml)
    {
        try
        {
            using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port);
            
            client.EnableSsl = _emailSettings.EnableSsl;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);
            
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Timeout = 30000;

            using var message = new MailMessage();
            message.From = new MailAddress(_emailSettings.FromEmail, _emailSettings.FromName);
            message.To.Add(to);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = isHtml;

            await client.SendMailAsync(message);
            
            _logger.LogInformation("Email sent successfully to: {To} at {Time}", to, DateTime.UtcNow);
        }
        catch (SmtpException smtpEx)
        {
            _logger.LogError(smtpEx, "Error at sending email to: {To}. StatusCode: {StatusCode}", to, smtpEx.StatusCode);
            throw new InvalidOperationException($"SMTP Fail: {smtpEx.Message}", smtpEx);
        }
    }
}
