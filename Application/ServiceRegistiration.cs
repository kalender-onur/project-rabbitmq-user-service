using Application.Interfaces;
using Application.Interfaces.AppUser;
using Application.Interfaces.RabbitMq;
using Application.Services;
using Application.Services.RabbitMq;
using Domain.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ServiceRegistiration
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMqSettings"));
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IRabbitMqService, RabbitMqService>();

            return services;
        }
    }
}
