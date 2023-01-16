using CompanyWebcast.Application.Common.DTOs;
using CompanyWebcast.Domain.WeatherForecast;

namespace CompanyWebcast.Application.Services
{
    public  interface IWeatherForecastService
    {
        public Task<WeatherForecast> AddWeatherForecast(WeatherForecastDTO weatherForecastDTO);
        public WeatherForecastResult UpdateWeatherForecast();
        public Task<List<WeatherForecast>> GetWeeklyWeatherForecast();
    }
}
