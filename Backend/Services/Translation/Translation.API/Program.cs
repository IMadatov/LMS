using Application;
using Infrastructure;
using Web.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .InfrastructureSettings(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddMainConfigureServices(builder.Configuration);

var app =builder.Build()
    .MainConfigure();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();


app.Run();


