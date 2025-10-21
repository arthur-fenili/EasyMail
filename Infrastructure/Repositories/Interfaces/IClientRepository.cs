using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces;

public interface IClientRepository
{
    void CreateClient(Client client);
}

