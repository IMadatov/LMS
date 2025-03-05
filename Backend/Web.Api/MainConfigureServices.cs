using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebAPI;

public static class MainConfigureServices
{
    public static IServiceCollection AddMainConfigureServices(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {
        services.AddControllers();
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
        services.AddAuthorization();

        services.AddHttpContextAccessor();

        services.Configure<IdentityOptions>(option =>
        {
            option.User.RequireUniqueEmail = false;

            //User
            option.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            //Lockout
            option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            option.Lockout.MaxFailedAccessAttempts = 10000;
            option.Lockout.AllowedForNewUsers = true;

            //Password


        });

        services.AddSwaggerGen();

        services.AddCors(opt =>
        {
            opt.AddPolicy("ToGlobal",
       buil => buil
           .WithOrigins("https://cgtlb6bz-4200.euw.devtunnels.ms").AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            opt.AddPolicy("ToLocal",
               buil => buil
                   .WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials());
        });



        return services;
    }

    public static WebApplication MainConfigure(this WebApplication app)
    {

        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerUI();
            app.UseSwagger();
        }

        app.UseHttpsRedirection();

        app.UseCors("ToLocal");
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}
