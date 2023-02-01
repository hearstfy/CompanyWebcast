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

        //public static WeatherForecastHourlyResponseDTO ToDTO(this WeatherForecastHourly forecastHourly)
        //{
        //    return new WeatherForecastHourlyResponseDTO()
        //    {
        //        StartHour = forecastHourly.StartHour,
        //        EndHour = forecastHourly.EndHour,
        //        TemperatureInCelcius = forecastHourly.TemperatureC,
        //        TemperatureInFahrenheit = forecastHourly.TemperatureF,
        //        Summary = forecastHourly.Summary

        //    };
        //}

        //public static WeatherForecastResponseDTO ToDTO(this WeatherForecast weatherForecast)
        //{
        //    return new WeatherForecastResponseDTO()
        //    {
        //        Id = weatherForecast.Id.Value,
        //        Date = weatherForecast.Date,
        //        HourlyForecasts = weatherForecast.HourlyForecasts.OrderBy(hf => hf.StartHour).ToList().ConvertAll(hf => hf.ToDTO())
        //    };
        //}

        //public static WeatherForecastHourly ToEntity(this AddWeatherForecastHourlyDTO weatherForecastHourlyDTO)
        //{
        //    return WeatherForecastHourly.Create(
        //             startHour: weatherForecastHourlyDTO.StartHour,
        //             endHour: weatherForecastHourlyDTO.EndHour,
        //             temperature: (double)weatherForecastHourlyDTO.TemperatureC);
        //}

        //public static WeatherForecast ToAggregate(this AddWeatherForecastDTO weatherForecastDTO)
        //{
        //    return WeatherForecast.Create(
        //        date: DateOnly.FromDateTime((DateTime)weatherForecastDTO.Date), 
        //        weatherForecastHourlies: weatherForecastDTO.WeatherForecastsHourly.ConvertAll(wfh => wfh.ToEntity()));
        //}

    }
}
