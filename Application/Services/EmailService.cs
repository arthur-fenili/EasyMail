using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Services.Interfaces;
using Microsoft.Extensions.Logging;


namespace Application.Services;

public class EmailService : IEmailService
{
    private readonly IEmailProvider _emailProvider;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IEmailProvider emailProvider, ILogger<EmailService> logger)
    {
        _emailProvider = emailProvider;
        _logger = logger;
    }

    public async Task<EmailResponse> SendEmailAsync(SendEmailRequest request)
    {
        _logger.LogInformation("Initiating e-mail sending to: {To}, Subject: {Subject}", request.To, request.Subject);

        try
        {
            await _emailProvider.SendAsync(request.To, request.Subject, request.Body, request.IsHtml);

            return new EmailResponse
            {
                Success = true,
                Message = "Email sent successfully.",
                SentAt = DateTime.UtcNow
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in sending e-mail to: {To}. Error: {ErrorMessage}", request.To, ex.Message);
            
            return new EmailResponse
            {
                Success = false,
                Message = $"Failed to send email: {ex.Message}",
                SentAt = DateTime.UtcNow
            };
        }
    }
}
