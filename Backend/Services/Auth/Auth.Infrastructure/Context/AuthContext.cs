using Auth.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Context;

public class AuthContext(DbContextOptions options) : IdentityDbContext<ApplicationUser, ApplicationRole,Guid>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .Entity<ApplicationUser>()
            .Property(x => x.UserName)
            .IsRequired(false);

        builder
            .Entity<ApplicationUser>()
            .HasIndex(x => x.UserName)
            .IsUnique()
            .HasFilter($"{nameof(ApplicationUser.TelegramId)} is not null");

        builder.Entity<StatusUser>()
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Entity<StatusUser>()
            .HasOne(x => x.ApplicationUser)
            .WithOne(y => y.StatusUser)
            .HasForeignKey<StatusUser>(su => su.ApplicationUserId);

        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AuthContextSeed).Assembly).Seed();
    }

    public DbSet<StatusUser> StatusUsers { get; set; }
}
