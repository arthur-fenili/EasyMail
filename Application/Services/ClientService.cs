using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Mappers;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<ClientResponse> CreateClient(CreateClientRequest request)
    {
        var client = ClientMapper.ToEntity(request);
        
        var createdClient = await _clientRepository.CreateClient(client);
        
        return ClientMapper.ToResponse(createdClient);
    }

    public async Task<IEnumerable<ClientResponse>> GetAllClients()
    {
        var clients = await _clientRepository.GetClientsAsync();
        return ClientMapper.ToResponseList(clients);
    }

    public async Task<ClientResponse> UpdateClientAsync(string id, UpdateClientRequest request)
    {
        var existingClient = await _clientRepository.GetClientByIdAsync(id) ?? throw new ArgumentException($"Client with ID {id} was not found", nameof(id));

        existingClient.Name = request.Name;
        existingClient.Email = request.Email;
        existingClient.IsActive = request.IsActive;

        var updatedClient = await _clientRepository.UpdateClientAsync(existingClient);
        return ClientMapper.ToResponse(updatedClient);
    }
}
