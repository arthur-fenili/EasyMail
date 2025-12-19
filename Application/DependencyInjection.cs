using Application.Services;
using Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Application Services
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IEmailService, EmailService>();
        
        return services;
    }
}