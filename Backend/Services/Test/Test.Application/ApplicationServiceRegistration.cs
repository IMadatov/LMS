using Microsoft.Extensions.DependencyInjection;
using Test.Application.FileInfo;

namespace Test.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IFileInfoService, FileInfoService>();
        return services;
    }
}
