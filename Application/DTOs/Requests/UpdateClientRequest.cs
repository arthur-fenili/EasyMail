using Domain.Entities;

namespace Application.DTOs.Requests
{
    public class UpdateClientRequest
    {
        public string Name { get; set; } = string.Empty;
        
        public string Email { get; set; } = string.Empty;
        
        public bool IsActive { get; set; } = true;
    }
}
