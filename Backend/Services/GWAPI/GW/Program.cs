using General;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


if(builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("ocelot.json",false,true);
}
else
{
    builder.Configuration.AddJsonFile("ocelot.prod.json",false, true);
}

builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddCors(option =>
{
    option.AddPolicy(
        "AllowOrigin",
        builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithOrigins(GlobalConstants.AllowedHosts);
    });
});

var app = builder.Build();

app.UseHttpsRedirection();


app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowOrigin");

app.UseWebSockets();

await app.UseOcelot();


app.Run();
