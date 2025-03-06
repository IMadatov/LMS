using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace Test.Infrastucture;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection InfrastructureSetting(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TestsContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Test"), b =>
            {
                b.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
        });
        return services;
    }

    public static async Task<WebApplication> MainConfigureMigration(this WebApplication app)
    {
        try
        {
            await using var scope = app.Services.CreateAsyncScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<TestsContext>();

            await dbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return app;
    }
}
