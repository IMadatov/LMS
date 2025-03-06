using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace Test.Infrastucture;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection InfrastructureSetting(this IServiceCollection services,IConfiguration configuration)
    {
        
        return services;
    }

    public static async  Task<WebApplication> MainConfigureMigration(this WebApplication app)
    {
        return app;
    }
}
