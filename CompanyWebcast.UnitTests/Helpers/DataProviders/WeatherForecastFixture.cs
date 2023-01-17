using CompanyWebcast.Application.Common.DTOs;
using CompanyWebcast.Domain.WeatherForecast;
using CompanyWebcast.Domain.WeatherForecast.Entities;
using System;
using System.Collections.Generic;

namespace CompanyWebcast.UnitTests.Helpers.DataProviders
{
    public static class WeatherForecastDataProvider
    {
        public static Random random = new Random();
        public static List<WeatherForecast> GetWeatherForecasts(int forecastSize)
        {
            var forecastHourlies = GetWeatherForecastHourlies(random.Next(1, 24));
            var forecasts = new List<WeatherForecast>();

            for (int i = 0; i < forecastSize; i++)
            {
                forecasts.Add(GetWeatherForecast(DateOnly.FromDateTime(DateTime.Now.AddDays(i)), forecastHourlies));
            }

            return forecasts;

        }

        public static WeatherForecast GetWeatherForecast(DateOnly date, List<WeatherForecastHourly> forecastHourlies)
        {
            return WeatherForecast.Create(date, forecastHourlies);
        }

        public static WeatherForecastHourly GetWeatherForecastHourly(int startHour, int endHour, double temperature)
        {
            return WeatherForecastHourly.Create(startHour, endHour, temperature);
        }

        public static List<WeatherForecastHourly> GetWeatherForecastHourlies(int size)
        {

            var forecastHourlies = new List<WeatherForecastHourly>();
            for (int i = 0; i < size; i++)
            {
                forecastHourlies.Add(GetWeatherForecastHourly(i, i + 1, random.Next(-60, 60)));
            }

            return forecastHourlies;
        }

        public static List<AddWeatherForecastHourlyDTO> GetAddWeatherForecastDTOs(int size)
        {
            var list = new List<AddWeatherForecastHourlyDTO>();
            for (var i = 0; i < size; i++)
            {
                list.Add(GetWeatherForecastHourlyDTO(i, i+1, random.Next(-60,60)));
            }

            return list;
        }

        public static AddWeatherForecastHourlyDTO GetWeatherForecastHourlyDTO(int startHour, int endHour, double temperature)
        {
            return new AddWeatherForecastHourlyDTO()
            {
                StartHour = startHour,
                EndHour = endHour,
                TemperatureC = temperature
            };
        }

        public static AddWeatherForecastDTO GetAddWeatherForecastDTO(DateTime? date)
        {
            var addHourlyDTOs = GetAddWeatherForecastHourlyDTOs(random.Next(1,24));
            return new AddWeatherForecastDTO()
            {
                Date = date ?? DateTime.Now,
                WeatherForecastsHourly = addHourlyDTOs
            };

        }

        public static List<AddWeatherForecastHourlyDTO> GetAddWeatherForecastHourlyDTOs(int size)
        {
            var list = new List<AddWeatherForecastHourlyDTO>();

            for(var i = 0; i < size; i++)
            {
                list.Add(GetAddWeatherForecastHourlyDTO(i, i+1 , random.Next(-60,60)));
            }

            return list;
        }

        public static AddWeatherForecastHourlyDTO GetAddWeatherForecastHourlyDTO(int startHour, int endHour, double temperature)
        {
            return new AddWeatherForecastHourlyDTO()
            {
                StartHour = startHour,
                EndHour = endHour,
                TemperatureC = temperature
            };
        }

        public static List<AddWeatherForecastHourlyDTO> GetWeatherForecastHourlyDTOs(int size)
        {
            var list = new List<AddWeatherForecastHourlyDTO>();
            for (var i = 0; i < size; i++)
            {
                list.Add(GetWeatherForecastHourlyDTO(i, i + 1, random.Next(-60, 60)));
            }

            return list;
        }
    }
}
