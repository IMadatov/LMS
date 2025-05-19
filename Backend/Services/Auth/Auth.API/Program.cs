using Auth.Application;
using Auth.Domain.Entities;
using Auth.Infrastructure;
using BaseCrud.Abstractions;
using Serilog;
using System.Reflection;
using Web.API;


var dockerHost = Convert.ToBoolean(value: Environment.GetEnvironmentVariable(variable: "DOTNET_RUNNING_IN_CONTAINER"));
bool nswagBuild = Environment.GetEnvironmentVariable("NSWAG_BUILD") is not null;
string hostBuilder = nswagBuild ? "NSwag:" : dockerHost ? "Docker" : "Dotnet";

MainConfigureServices.ConfigureBootstrapSerilog();

Log.Information("Auth API starting...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    ConfigureServices(builder);

    var app = builder.Build();

    Configure(app);

    app.Run();

}
catch(Exception ex)
{
    Log.Fatal(ex, "Auth API failed to start");
}
finally
{
    Log.Information("Auth API is shutting down!");
    Log.CloseAndFlush();
}   


void ConfigureServices(WebApplicationBuilder builder)
{
    IServiceCollection services = builder.Services;
    builder.Configuration.AddJsonFile($"serilog.{builder.Environment.EnvironmentName}.json",
        optional: true,
        reloadOnChange: true);

    builder.Host.UseSerilog();


    // Add services to the container.
    builder.Services
        .InfrastructureServices(builder.Configuration)
        .AddApplicationService(builder.Configuration)
        .AddMainConfigureServices(builder.Configuration);

    builder.Services.AddBaseCrudService(new BaseCrud.BaseCrudServiceOptions
    {
        Assemblies = [
            Assembly.GetExecutingAssembly(),
            Assembly.GetAssembly(typeof(ApplicationServicesRegistration))!,
            Assembly.GetAssembly(typeof(ApplicationUser))!,
            Assembly.GetAssembly(typeof(InfrastructureServiceRegistration))!
        ]
    });

}

async void Configure(WebApplication app) 
{
    app = await app
    .MainConfigure()
    .AuthApplicationConfig()
    .MainConfigurationMigrationAsync();
}





/* 
System.AggregateException: 'Some services are not able to be constructed (Error while validating the service descriptor 'ServiceType: Auth.Application.AuthServices.IAuthService Lifetime: Scoped ImplementationType: Auth.Application.AuthServices.AuthService': Unable to resolve service for type 'Microsoft.AspNetCore.Identity.UserManager`1[Auth.Domain.Entities.ApplicationUser]' while attempting to activate 'Auth.Application.AuthServices.AuthService'.)'

 */