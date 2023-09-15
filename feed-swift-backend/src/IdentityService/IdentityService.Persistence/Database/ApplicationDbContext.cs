using IdentityService.Domain.Entities;
using IdentityService.Persistence.Database.Configs;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Persistence.Database;

public class ApplicationDbContext:DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfig());
    }
}