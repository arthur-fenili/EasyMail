using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Requests;

public class EmailAllClientsRequest
{
    [Required]
    public string Subject { get; set; } = string.Empty;

    [Required]
    public string Body { get; set; } = string.Empty;

    [Required]
    public bool IsHtml { get; set; } = false;
}
