using Application.DTOs.Requests;
using Application.DTOs.Responses;

namespace Application.Services.Interfaces;

public interface IEmailService
{
    Task<EmailResponse> SendEmailAsync(SendEmailRequest request);
}
