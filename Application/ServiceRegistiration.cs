using Application.Interfaces;
using Application.Interfaces.AppUser;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ServiceRegistiration
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<IAppUserService, AppUserService>();
            return services;
        }
    }
}
