using System.ComponentModel.DataAnnotations;


namespace Application.DTOs.Requests;

public class SendEmailRequest
{
    [Required]
    [EmailAddress]
    public string To { get; set; } = string.Empty;
    
    [Required]
    public string Subject { get; set; } = string.Empty;

    [Required]
    public string Body { get; set; } = string.Empty;
    
    [Required]
    public bool IsHtml { get; set; } = false;
}
