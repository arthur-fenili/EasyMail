using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly IMongoCollection<Client> _clients;

    public ClientRepository(MongoDbContext context)
    {
        _clients = context.Clients;
    }

    public async Task<Client> CreateClient(Client client)
    {
        await _clients.InsertOneAsync(client);
        return client;
    }

    public async Task<IEnumerable<Client>> GetClientsAsync()
    {
        return await _clients.Find(c => true && c.IsActive == true).ToListAsync();
    }

    public async Task<Client?> GetClientByIdAsync(string id)
    {
        return await _clients.Find(c => c.Id.ToString() == id && c.IsActive == true).FirstOrDefaultAsync();
    }

    public async Task<Client> UpdateClientAsync(Client client)
    {
        await _clients.ReplaceOneAsync(c => c.Id == client.Id && c.IsActive == true, client);
        return client;
    }
}

