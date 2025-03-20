using Auth.Application;
using Auth.Infrastructure;
using Web.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services
    .InfrastructureServices(builder.Configuration)
    .AddApplicationService(builder.Configuration)
    .AddMainConfigureServices(builder.Configuration);


var app = await builder.Build()
.MainConfigure()
.MainConfigurationMigrationAsync();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();



app.Run();

/* 
System.AggregateException: 'Some services are not able to be constructed (Error while validating the service descriptor 'ServiceType: Auth.Application.AuthServices.IAuthService Lifetime: Scoped ImplementationType: Auth.Application.AuthServices.AuthService': Unable to resolve service for type 'Microsoft.AspNetCore.Identity.UserManager`1[Auth.Domain.Entities.ApplicationUser]' while attempting to activate 'Auth.Application.AuthServices.AuthService'.)'

 */