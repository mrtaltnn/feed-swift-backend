using IdentityService.Application.Model;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Domain;

public static class DomainRegistration
{
    public static IServiceCollection  AddDomain(this IServiceCollection services, AppSettings appSettings)
    {

        return services;
    }
}

