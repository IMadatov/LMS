using TelegramBot.Application;
using TelegramBot.Infrastructure;
using Web.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.InfrastructureService(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddMainConfigureServices(builder.Configuration);


var app = builder.Build().MainConfigure();

app.Run();

