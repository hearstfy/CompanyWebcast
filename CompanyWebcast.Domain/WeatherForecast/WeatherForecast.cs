using CompanyWebcast.Domain.Common.Models;
using CompanyWebcast.Domain.WeatherForecast.Entities;
using CompanyWebcast.Domain.WeatherForecast.ValueObjects;

namespace CompanyWebcast.Domain.WeatherForecast
{
    public sealed class WeatherForecast : AggregateRoot<WeatherForecastId>
    {
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
        public static WeatherForecast Create(DateOnly date, List<WeatherForecastHourly> weatherForecastHourlies )
        {
            return new WeatherForecast(WeatherForecastId.Create(Guid.NewGuid()),date, weatherForecastHourlies, DateTime.Now, DateTime.Now);
        }

        public void UpdateHourlyForecasts(List<WeatherForecastHourly> forecastHourlies)
        {
            UpdateExistingHourlies(forecastHourlies);
            AddNewHourlies(forecastHourlies);
            HourlyForecasts.OrderBy(hf => hf.StartHour);
        }

        private void UpdateExistingHourlies(List<WeatherForecastHourly> forecastHourlies)
        {
            foreach(var forecastHourly in forecastHourlies)
            {
                var existingHourly = HourlyForecasts.FirstOrDefault(hf => hf.StartHour == forecastHourly.StartHour);
                if(existingHourly != null)
                {
                    existingHourly.SetTemperature(forecastHourly.TemperatureC);
                }
            }
        }

        private void AddNewHourlies(List<WeatherForecastHourly> forecastHourlies)
        {
            foreach (var forecastHourly in forecastHourlies)
            {
                var existingHourly = HourlyForecasts.FirstOrDefault(hf => hf.StartHour == forecastHourly.StartHour);
                if (existingHourly == null)
                {
                    HourlyForecasts.Add(WeatherForecastHourly.Create(forecastHourly.StartHour, forecastHourly.EndHour, forecastHourly.TemperatureC));
                }
            }
        }

#pragma warning disable CS8618
        private WeatherForecast() { }

    #pragma warning restore CS8618
    }
}
