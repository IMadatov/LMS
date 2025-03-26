using Auth.Application;
using Auth.Domain.Entities;
using Auth.Infrastructure;
using BaseCrud.Abstractions;
using System.Reflection;
using Web.API;

var builder = WebApplication.CreateBuilder(args);

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


var app = await builder.Build()
.MainConfigure()
.AuthApplicationConfig()
.MainConfigurationMigrationAsync();


app.Run();

/* 
System.AggregateException: 'Some services are not able to be constructed (Error while validating the service descriptor 'ServiceType: Auth.Application.AuthServices.IAuthService Lifetime: Scoped ImplementationType: Auth.Application.AuthServices.AuthService': Unable to resolve service for type 'Microsoft.AspNetCore.Identity.UserManager`1[Auth.Domain.Entities.ApplicationUser]' while attempting to activate 'Auth.Application.AuthServices.AuthService'.)'

 */