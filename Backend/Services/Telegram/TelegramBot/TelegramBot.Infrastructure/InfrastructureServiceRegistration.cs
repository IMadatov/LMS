using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TelegramBot.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection InfrastructureService(this IServiceCollection services,IConfiguration configuration)
    {

        return services;
    }
}
