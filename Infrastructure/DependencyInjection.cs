using Domain.Interfaces;
using Infrastructure.Configuration;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Infrastructure Services
        services.AddSingleton<MongoDbContext>();
        
        // Repositories
        services.AddScoped<IClientRepository, ClientRepository>();
        
        // External Services
        services.AddScoped<IEmailProvider, SmtpEmailProvider>();
        
        return services;
    }
}