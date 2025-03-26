using Auth.Application.Services;
using Auth.Application.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplicationService(this  IServiceCollection services,IConfiguration configuration)
    {
        services.AddSignalR();
        services.AddScoped<IAuthService, AuthService>();    
        return services;
    }

    public static  WebApplication AuthApplicationConfig(this WebApplication app)
    {
        app.MapHub<AuthHub>("/api/auth/signalR");
        return app;
    }
}
