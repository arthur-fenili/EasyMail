using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Mappers;
using Application.Services.Interfaces;
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
}
