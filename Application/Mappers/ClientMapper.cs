using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Domain.Entities;

namespace Application.Mappers;

public static class ClientMapper
{
    public static Client ToEntity(CreateClientRequest request)
    {
        return new Client(request.Name, request.Email, request.IsActive);
    }

    public static ClientResponse ToResponse(Client client)
    {
        return new ClientResponse
        {
            Id = client.Id.ToString(),
            Name = client.Name,
            Email = client.Email,
            IsActive = client.IsActive
        };
    }

    public static IEnumerable<ClientResponse> ToResponseList(IEnumerable<Client> clients)
    {
        return clients.Select(ToResponse);
    }
}