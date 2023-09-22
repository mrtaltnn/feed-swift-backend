using IdentityService.Application.Model;
using IdentityService.Domain.Interfaces.Repositories;
using IdentityService.Persistence.Database;
using IdentityService.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Persistence;

public static class PersistenceRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, AppSettings appSettings)
    {
        services.AddDbContext<ApplicationDbContext>(
            options =>
            {
                options.UseNpgsql(appSettings.PostgresDbSettings.ConnectionString, m =>
                {
                    m.EnableRetryOnFailure(maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorCodesToAdd: null);
                });
            },
            ServiceLifetime.Transient);
        
        //Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        
        //Core
        services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationDbContext>>();
        
        return services;
    }

    public static void UsePersistence(this IApplicationBuilder app)
    {
        var serviceProvider = app.ApplicationServices;
        using var serviceScope = serviceProvider.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
        context?.Database.Migrate();
    }
}