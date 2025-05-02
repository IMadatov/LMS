using Application;
using Infrastructure;
using Serilog;
using Web.API;

var dockerHost = Convert.ToBoolean(value: Environment.GetEnvironmentVariable(variable: "DOTNET_RUNNING_IN_CONTAINER"));
bool nswagBuild = Environment.GetEnvironmentVariable("NSWAG_BUILD") is not null;
string hostBuilder = nswagBuild ? "NSwag:" : dockerHost ? "Docker" : "Dotnet";

MainConfigureServices.ConfigureBootstrapSerilog();

Log.Information("Translation API starting...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    ConfigureServices(builder);

    var app = builder.Build();
       
    Configure(app);

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Translation API failed to start");
}
finally
{
    Log.Information("Translation API is shutting down!");
    Log.CloseAndFlush();
}

void ConfigureServices(WebApplicationBuilder builder)
{
    IServiceCollection services = builder.Services;
    builder.Configuration.AddJsonFile($"serilog.{builder.Environment.EnvironmentName}.json",
        optional: true,
        reloadOnChange: true);

    builder.Host.UseSerilog();
    builder.Services
        .InfrastructureSettings(builder.Configuration)
        .AddApplicationServices(builder.Configuration)
        .AddMainConfigureServices(builder.Configuration);
}
void Configure(WebApplication app)
{
    app = app.MainConfigure();
    //app.UseHttpsRedirection();
}