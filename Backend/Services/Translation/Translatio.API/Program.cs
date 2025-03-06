using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .InfrastructureSettings(builder.Configuration)
    .AddApplicationServices(builder.Configuration);



var app =await builder.Build().MainConfigurationInfrastructureAsync();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Run();
