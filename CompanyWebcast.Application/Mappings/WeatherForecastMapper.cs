using CompanyWebcast.Application.Common.Requests;
using CompanyWebcast.Application.Common.Responses;
using CompanyWebcast.Domain.WeatherForecast;
using CompanyWebcast.Domain.WeatherForecast.Entities;

namespace CompanyWebcast.Application.Mappings
{
    public static class WeatherForecastMapper
    {
        public static WeatherForecast ToAggregate(this AddWeatherForecastRequest request)
        {
            return WeatherForecast.Create(request.Date, request.Hourlies.ConvertAll(h => h.ToEntity()));
        }

        public static WeatherForecastHourly ToEntity(this AddUpdateWeatherForecastHourlyRequest request)
        {
            return WeatherForecastHourly.Create(request.StartHour, request.EndHour, request.TemperatureC);
        }

        public static AddWeatherForecastResponse ToResponse(this WeatherForecast weatherForecast)
        {
            return new AddWeatherForecastResponse()
            {
                Id = weatherForecast.Id.Value,
                Date = weatherForecast.Date,
                HourlyForecasts = weatherForecast.HourlyForecasts.ConvertAll(hf => hf.ToResponse())
            };
        }

        public static AddUpdateWeatherForecastHourlyResponse ToResponse(this WeatherForecastHourly forecastHourly)
        {
            return new AddUpdateWeatherForecastHourlyResponse()
            {
                StartHour = forecastHourly.StartHour,
                EndHour = forecastHourly.EndHour,
                Summary = forecastHourly.Summary,
                TemperatureInCelcius = forecastHourly.TemperatureC,
                TemperatureInFahrenheit = forecastHourly.TemperatureF
            };
        }
    }
}
