using CompanyWebcast.Domain.WeatherForecast;
using CompanyWebcast.Infrastructure.Persistance;
using CompanyWebcast.UnitTests.Fixtures;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CompanyWebcast.UnitTests.Helpers.Extensions
{
    public static class ApplicationDBContextExtensions
    {
        public static ApplicationDBContext Populate(this ApplicationDBContext context, int size)
        {
            var forecasts = WeatherForecastFixture.GetWeatherForecasts(size);
            context.WeatherForecasts.AddRange(forecasts);
            context.SaveChanges();

            return context;
        }

        public static ApplicationDBContext Populate(this ApplicationDBContext context, WeatherForecast forecast)
        {
            context.WeatherForecasts.AddRange(forecast);
            context.SaveChanges();

            return context;
        }

        public static ApplicationDBContext DeleteAll(this ApplicationDBContext context)
        {
            var records = context.WeatherForecasts.Include(wf=> wf.HourlyForecasts).ToList();
            records.ForEach(wf => wf.HourlyForecasts.Clear());
            context.WeatherForecasts.RemoveRange(records);
            context.SaveChanges();
            return context;
        }

    }
}
