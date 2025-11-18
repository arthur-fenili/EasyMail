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
        var clients = await _clientRepository.GetClientsAsync();

        if (clients != null)
        {
            foreach (Client client in clients)
            {
                await _emailProvider.SendAsync(client.Email, request.Subject, request.Body, request.IsHtml);
            }

            return new EmailResponse
            {
                Success = true,
                Message = "Email sent successfully to all clients in database.",
                SentAt = DateTime.UtcNow
            };
        }

        return new EmailResponse
        {
            Success = false,
            Message = "No clients found in database.",
        };
    }
}
