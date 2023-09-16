using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Domain;

public static class DomainRegistration
{
    public static IServiceCollection  AddDomain(this IServiceCollection services, IConfiguration configuration)
    {

        return services;
    }
}

