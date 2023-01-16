using CompanyWebcast.Domain.Common.Models;
using CompanyWebcast.Domain.WeatherForecast.Entities;
using CompanyWebcast.Domain.WeatherForecast.ValueObjects;

namespace CompanyWebcast.Domain.WeatherForecast
{
    public sealed class WeatherForecast : AggregateRoot<WeatherForecastId>
    {
        private readonly List<WeatherForecastHourly> _hourlyForecast = new();

        public DateOnly Date { get; private set; }
        public List<WeatherForecastHourly> HourlyForecasts { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime ModifiedAt { get; private set; }

        private WeatherForecast(WeatherForecastId weatherForecastId, DateOnly date, List<WeatherForecastHourly> weatherForecastHourlies,  DateTime createdAt, DateTime modifiedAt)
            : base(weatherForecastId)
        {
            Date = date;
            HourlyForecasts = weatherForecastHourlies;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
        }

        //public static WeatherForecast Create()
        //{
        //    return new WeatherForecast(WeatherForecastId.Create(Guid.NewGuid()), DateTime.UtcNow, DateTime.UtcNow);
        //}

        public static WeatherForecast Create(DateOnly date, List<WeatherForecastHourly> weatherForecastHourlies )
        {
            return new WeatherForecast(WeatherForecastId.Create(Guid.NewGuid()),date, weatherForecastHourlies, DateTime.Now, DateTime.Now);
        }

        public void UpdateHourlyForecasts(List<WeatherForecastHourly> forecastHourlies)
        {
            this.HourlyForecasts = forecastHourlies;
        }

#pragma warning disable CS8618
        private WeatherForecast() { }

    #pragma warning restore CS8618
    }
}
