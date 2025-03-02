using Application.Services;
using Domain.Interfaces.Services;
using Domain.ModelDtos;
using Domain.Models;
using GTranslate.Services;
using GTranslate.Services.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAutoMapper(opt =>
            {
                opt.CreateMap<Transloco,TranslocoDto>().ReverseMap(); 
            });
            services.AddScoped<ITranslocoService, TranslocoService>();
            services.AddScoped<IGTranslateService, GTranslateService>(); 
            return services;
        }
    }
}
