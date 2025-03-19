using Auth.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplicationService(this  IServiceCollection services,IConfiguration configuration)
    {
        services.AddScoped<IAuthService,AuthService>();
        services.AddScoped<IUserService, UserService>();
        return services;
    }
}
