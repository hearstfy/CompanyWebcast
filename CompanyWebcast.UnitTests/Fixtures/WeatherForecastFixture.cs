using CompanyWebcast.Application.Common.Requests;
using CompanyWebcast.Domain.WeatherForecast;
using CompanyWebcast.Domain.WeatherForecast.Entities;
using System;
using System.Collections.Generic;

namespace CompanyWebcast.UnitTests.Fixtures
{
    public static class WeatherForecastFixture
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

        public static List<AddUpdateWeatherForecastHourlyRequest> GetAddWeatherForecastRequests(int size)
        {
            var list = new List<AddUpdateWeatherForecastHourlyRequest>();
            for (var i = 0; i < size; i++)
            {
                list.Add(GetWeatherForecastHourlyRequest(i, i + 1, random.Next(-60, 60)));
            }

            return list;
        }

        public static AddUpdateWeatherForecastHourlyRequest GetWeatherForecastHourlyRequest(int startHour, int endHour, double temperature)
        {
            return new AddUpdateWeatherForecastHourlyRequest()
            {
                StartHour = startHour,
                EndHour = endHour,
                TemperatureC = temperature
            };
        }

        public static AddWeatherForecastRequest GetAddWeatherForecastRequest(DateTime? date)
        {
            var addHourlies = GetAddWeatherForecastHourlyRequests(random.Next(1, 24));
            return new AddWeatherForecastRequest()
            {
                Date = date != null ? DateOnly.FromDateTime((DateTime)date) : DateOnly.FromDateTime(DateTime.Now),
                Hourlies = addHourlies
            };

        }

        public static List<AddUpdateWeatherForecastHourlyRequest> GetAddWeatherForecastHourlyRequests(int size)
        {
            var list = new List<AddUpdateWeatherForecastHourlyRequest>();

            for (var i = 0; i < size; i++)
            {
                list.Add(GetAddWeatherForecastHourlyRequest(i, i + 1, random.Next(-60, 60)));
            }

            return list;
        }

        public static AddUpdateWeatherForecastHourlyRequest GetAddWeatherForecastHourlyRequest(int startHour, int endHour, double temperature)
        {
            return new AddUpdateWeatherForecastHourlyRequest()
            {
                StartHour = startHour,
                EndHour = endHour,
                TemperatureC = temperature
            };
        }
    }
}
