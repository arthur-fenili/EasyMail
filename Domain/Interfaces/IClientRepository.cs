using Domain.Entities;

namespace Domain.Interfaces;

public interface IClientRepository
{
    Task<Client> CreateClient(Client client);
    Task<IEnumerable<Client>> GetClientsAsync();
    Task<Client?> GetClientByIdAsync(string id);
    Task<Client> UpdateClientAsync(Client client);
}

