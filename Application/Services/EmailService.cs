using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;


namespace Application.Services;

public class EmailService(IEmailProvider emailProvider, IClientRepository clientRepository, ILogger<EmailService> logger) : IEmailService
{
    private readonly IEmailProvider _emailProvider = emailProvider;
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly ILogger<EmailService> _logger = logger;

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

    public async Task<EmailResponse> EmailAllClientsAsync(EmailAllClientsRequest request)
    {
        try
        {
            var clients = await _clientRepository.GetClientsAsync();
            
            if (!clients?.Any() ?? true)
            {
                return new EmailResponse
                {
                    Success = false,
                    Message = "No active clients found in database.",
                    SentAt = DateTime.UtcNow
                };
            }

            var clientList = clients!.ToList();
            var successCount = 0;
            var failureCount = 0;
            var errors = new List<string>();

            _logger.LogInformation("Starting bulk email to {ClientCount} clients with subject: {Subject}", 
                clientList.Count, request.Subject);

            foreach (var client in clientList)
            {
                try
                {
                    await _emailProvider.SendAsync(client.Email, request.Subject, request.Body, request.IsHtml);
                    successCount++;
                    
                    _logger.LogDebug("Email sent successfully to {ClientEmail}", client.Email);
                }
                catch (Exception ex)
                {
                    failureCount++;
                    var errorMessage = $"Failed to send email to {client.Email}: {ex.Message}";
                    errors.Add(errorMessage);
                    
                    _logger.LogWarning(ex, "Failed to send email to {ClientEmail}", client.Email);
                }
            }

            var isSuccess = successCount > 0;
            var message = failureCount == 0 
                ? $"Email sent successfully to all {successCount} clients."
                : $"Email sent to {successCount} clients. {failureCount} failures occurred.";

            if (errors.Any())
            {
                _logger.LogWarning("Bulk email completed with {SuccessCount} successes and {FailureCount} failures", 
                    successCount, failureCount);
            }

            return new EmailResponse
            {
                Success = isSuccess,
                Message = message,
                SentAt = DateTime.UtcNow
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while processing bulk email request");
            
            return new EmailResponse
            {
                Success = false,
                Message = $"Failed to process bulk email request: {ex.Message}",
                SentAt = DateTime.UtcNow
            };
        }
    }
}
