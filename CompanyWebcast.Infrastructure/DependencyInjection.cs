using CompanyWebcast.Application.Common.Interfaces.Persistance;
using CompanyWebcast.Infrastructure.Persistance;
using CompanyWebcast.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CompanyWebcast.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            var serverVersion = new MariaDbServerVersion(ServerVersion.AutoDetect(configuration.GetConnectionString("MariaDB")));

            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseMySql(configuration.GetConnectionString("MariaDB"), serverVersion)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
            });

            services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();

            return services;
        }
    }
}
