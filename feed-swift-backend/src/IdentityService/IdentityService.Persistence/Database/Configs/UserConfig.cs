using IdentityService.Domain.Constants;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Persistence.Database.Configs;

public sealed class UserConfig: BaseConfig<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        
        builder
            .Property(user => user.Email)
            .IsRequired()
            .HasMaxLength(MaxLengths.User.Email);

        builder
            .Property(user => user.FirstName)
            .IsRequired()
            .HasMaxLength(MaxLengths.User.FirstName);

        builder
            .Property(user => user.LastName)
            .IsRequired()
            .HasMaxLength(MaxLengths.User.LastName);

        builder
            .Property(user => user.Password)
            .IsRequired()
            .HasMaxLength(MaxLengths.User.Password);
        
        builder
            .Property(user => user.PhoneNumber)
            .IsRequired()
            .HasMaxLength(MaxLengths.User.PhoneNumber); 
        
        builder
            .Property(user => user.DeviceId)
            .IsRequired()
            .HasMaxLength(MaxLengths.User.DeviceId);

        builder.HasData(new User(
            1,
            "admin@email.com",
            "Admin",
            "User",
            // !Password123#
            "$2a$12$Blal/uiFIJdYsCLTMUik/egLbfg3XhbnxBC6Sb5IKz2ZYhiU/MzL2",
            "+000000000001",
            role:UserRole.Admin,
            deviceId:"0000"
            ));
    }
}