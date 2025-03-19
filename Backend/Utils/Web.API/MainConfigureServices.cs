using BaseCrud.PrimeNg;
using Clients;
using Clients.TelegramBot.Client;
using General;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web.API.Extensions.Converters;

namespace Web.API;

public static class MainConfigureServices
{
    public static IServiceCollection AddMainConfigureServices(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {
        //serilog https://github.com/serilog/serilog/wiki/Configuration-Basics


        // Adding Authentication  
        services.AddAuthentication()
            .AddCookie(IdentityConstants.ApplicationScheme, opt =>
                {

                    opt.ExpireTimeSpan = TimeSpan.FromHours(3);
                    opt.Events.OnRedirectToLogin = (context) =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    };
                    opt.Events.OnRedirectToAccessDenied = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        return Task.CompletedTask;
                    };
                    opt.Cookie.SameSite = SameSiteMode.None;
                    opt.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddOpenApiDocument();

        services.AddCors(options =>
        {
            options.AddPolicy(
                "AllowOrigin",
                builder =>
                {
                    builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithOrigins(GlobalConstants.AllowedHosts);
                });
        });

        //services.AddSignalR();

        services
            .AddControllers()
           .AddJsonOptions(o =>
           {
               o.JsonSerializerOptions.Converters.Add(new FilterMetadataConverter());
               o.JsonSerializerOptions.Converters.Add(new PrimeTableMetaConverter());
               o.JsonSerializerOptions.Converters.Add(new GuidConverter());
           });
        services.AddAutoMapper(opt =>
        {

        });

        services.AddHttpClient<TelegramBotClient>(client =>
        {
            client.BaseAddress = ServicesURL.TelegramBot;
        });


        services.AddHttpContextAccessor();



        return services;
    }

    public static WebApplication MainConfigure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();

        }

        //app.UseHttpsRedirection();

        app.UseCors("AllowOrigin");

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();


        app.MapControllers();

        return app;
    }
}