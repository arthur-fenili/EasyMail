using Domain.Commom;

namespace Domain.Entities;

public class Client(string name, string email, bool isActive) : BaseEntity
{
    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
    public bool IsActive { get; set; } = isActive;
}

