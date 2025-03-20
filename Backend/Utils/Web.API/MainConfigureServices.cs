using BaseCrud.PrimeNg;
using Clients;
using Clients.TelegramBot.Client;
using General;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
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
        //services.AddAuthentication()
        //    .AddCookie(IdentityConstants.ApplicationScheme, opt =>
        //        {

        //            opt.ExpireTimeSpan = TimeSpan.FromHours(3);
        //            opt.Events.OnRedirectToLogin = (context) =>
        //            {
        //                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        //                return Task.CompletedTask;
        //            };
        //            opt.Events.OnRedirectToAccessDenied = context =>
        //            {
        //                context.Response.StatusCode = StatusCodes.Status403Forbidden;
        //                return Task.CompletedTask;
        //            };
        //            opt.Cookie.SameSite = SameSiteMode.None;
        //            opt.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        //        });


        var jwtSettings = configuration.GetSection("JwtSettings");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"] ?? string.Empty))
            };
        });
        services.AddEndpointsApiExplorer();

        services.AddHttpContextAccessor();
        services.AddOpenApiDocument();
        services.AddSwaggerGen(o =>
        {
            o.CustomSchemaIds(id =>
            {
                return id.FullName!.Replace("+", "-");
            });

            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter 'Bearer' [space] and then your token in the text input below.\nExample",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme=JwtBearerDefaults.AuthenticationScheme,
                BearerFormat="JWT"
            };
            o.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme,securityScheme);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference=new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id=JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    []
                }
            };
            
            o.AddSecurityRequirement(securityRequirement);

        });


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

        services.AddTransient<AuthorizationHeaderHandler>();

        services.AddHttpClient<TelegramBotClient>((sp, client) =>
        {
            client.BaseAddress = ServicesURL.TelegramBot;
        }).AddHttpMessageHandler<AuthorizationHeaderHandler>();


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