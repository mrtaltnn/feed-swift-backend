using System.Reflection;
using Google.Protobuf.Collections;
using IdentityService.Application.DTOs.User;
using IdentityService.Domain.Entities;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Application.Model;

public static class MapsterRegistration
{
    public static IServiceCollection AddMapster(this IServiceCollection services, AppSettings appSettings)
    {
        var config = new TypeAdapterConfig();
        
        config.Scan(Assembly.GetEntryAssembly() ?? throw new InvalidOperationException());
        config.Default.NameMatchingStrategy(NameMatchingStrategy.Flexible);
        config.Default.UseDestinationValue(member =>
            member is { SetterModifier: AccessModifier.None, Type.IsGenericType: true } &&
            member.Type.GetGenericTypeDefinition() == typeof(RepeatedField<>));

        config.NewConfig<CreateUserDto, User>()
            .MapToConstructor(true)
            .IgnoreNullValues(true);
        
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }
}