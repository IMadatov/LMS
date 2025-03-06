using Test.Application;
using Test.Infrastucture;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .InfrastructureSetting(builder.Configuration)
    .AddApplicationServices()
    .AddMainConfigureServices(builder.Configuration);



var app = builder.Build()
    .MainConfigure();

// Configure the HTTP request pipeline.


app.Run();