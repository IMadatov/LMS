using Infrastructure.DbContextOptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection InfrastructureSettings(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<TranslationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("TranslationConnectionString"));
        });
        return services;
    }

    public static async Task<WebApplication> MainConfigurationInfrastructureAsync(this WebApplication app)
    {
        if(app.Environment.IsDevelopment())
        {   
            return app;
        }
        try
        {
            await using var serviceScope = app.Services.CreateAsyncScope();
            var translationContext = serviceScope.ServiceProvider.GetRequiredService<TranslationDbContext>();

            await translationContext.Database.MigrateAsync();
       
        }catch (Exception e)
        {
            Console.WriteLine(e);   
        }

        return app;
    }
}   
