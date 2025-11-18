using Application.DTOs.Requests;
using Application.DTOs.Responses;

namespace Application.Services.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientResponse>> GetAllClients();
        Task<ClientResponse> CreateClient(CreateClientRequest request);
        Task<ClientResponse> UpdateClientAsync(string id, UpdateClientRequest request);
    }
}
