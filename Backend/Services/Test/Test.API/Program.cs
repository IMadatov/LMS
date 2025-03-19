using Web.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMainConfigureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Run();