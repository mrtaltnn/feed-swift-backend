using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace IdentityService.Persistence;

public static class PersistenceRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        //TODO db context
        return services;
    }

    public static void UsePersistence(this IApplicationBuilder app)
    {
        //TODO Db migration
    }
}