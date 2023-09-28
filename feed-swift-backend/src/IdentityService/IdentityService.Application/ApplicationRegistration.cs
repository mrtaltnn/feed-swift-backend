using IdentityService.Application.Model;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Application;

public static class ApplicationRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services, AppSettings appSettings)
    {
        services.AddMapster(appSettings);
        
        return services;
    }
}