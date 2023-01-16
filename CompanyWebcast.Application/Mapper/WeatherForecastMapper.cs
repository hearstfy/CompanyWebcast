using CompanyWebcast.Application.Common.DTOs;
using CompanyWebcast.Domain.WeatherForecast;
using CompanyWebcast.Domain.WeatherForecast.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWebcast.Application.Mapper
{
    public static class WeatherForecastMapper
    {
        public static WeatherForecastHourlyResponseDTO ToDTO(this WeatherForecastHourly forecastHourly)
        {
            return new WeatherForecastHourlyResponseDTO()
            {
                StartHour = forecastHourly.StartHour,
                EndHour = forecastHourly.EndHour,
                TemperatureInCelcius = forecastHourly.TemperatureC,
                TemperatureInFahrenheit = forecastHourly.TemperatureF,
                Summary = forecastHourly.Summary

            };
        }

        public static WeatherForecastResponseDTO ToDTO(this WeatherForecast weatherForecast)
        {
            return new WeatherForecastResponseDTO()
            {
                Id = weatherForecast.Id.Value,
                Date = weatherForecast.Date,
                HourlyForecasts = weatherForecast.HourlyForecasts.OrderBy(hf => hf.StartHour).ToList().ConvertAll(hf => hf.ToDTO())
            };
        }

        public static WeatherForecastHourly ToEntity(this AddWeatherForecastHourlyDTO weatherForecastHourlyDTO)
        {
            return WeatherForecastHourly.Create(
                     startHour: weatherForecastHourlyDTO.StartHour,
                     endHour: weatherForecastHourlyDTO.EndHour,
                     temperature: (double)weatherForecastHourlyDTO.TemperatureC);
        }

        public static WeatherForecast ToAggregate(this AddWeatherForecastDTO weatherForecastDTO)
        {
            return WeatherForecast.Create(
                date: DateOnly.FromDateTime((DateTime)weatherForecastDTO.Date), 
                weatherForecastHourlies: weatherForecastDTO.WeatherForecastsHourly.ConvertAll(wfh => wfh.ToEntity()));
        }

    }
}
