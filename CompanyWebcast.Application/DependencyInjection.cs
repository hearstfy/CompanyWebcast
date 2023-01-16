using CompanyWebcast.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyWebcast.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();

            return services;
        }
    }
}
