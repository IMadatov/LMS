using Auth.Domain.Entities;
using Auth.Infrastructure.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Auth.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection InfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuthContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("AuthConnection"),
                builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null);
                }));

        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<AuthContext>()
            .AddDefaultTokenProviders();

        //services.AddIdentityCore<ApplicationUser>(options =>
        //{
        //    //user
        //    options.User.RequireUniqueEmail = false;
        //    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        //    // Lockout
        //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
        //    options.Lockout.MaxFailedAccessAttempts = 10000;
        //    options.Lockout.AllowedForNewUsers = true;

        //})
        //    .AddRoles<ApplicationRole>()
        //    .AddEntityFrameworkStores<AuthContext>()
        //    .AddClaimsPrincipalFactory<CustomUserClaimsPrincipalFactory>()
        //    .AddApiEndpoints() ;

        //services.AddIdentity<ApplicationUser, ApplicationRole>(option =>
        //{

        //    //user
        //    option.User.RequireUniqueEmail = false;
        //    option.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        //    // Lockout
        //    option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
        //    option.Lockout.MaxFailedAccessAttempts = 10000;
        //    option.Lockout.AllowedForNewUsers = true;
        //}).AddEntityFrameworkStores<AuthContext>()
        //.AddDefaultTokenProviders() ;
        //.AddClaimsPrincipalFactory<CustomUserClaimsPrincipalFactory>()


        return services;
    }

    public static async Task<WebApplication> MainConfigurationMigrationAsync(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            return app;
        }

        try
        {
            await using var scope = app.Services.CreateAsyncScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<AuthContext>();

            await dbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return app;
    }

}
