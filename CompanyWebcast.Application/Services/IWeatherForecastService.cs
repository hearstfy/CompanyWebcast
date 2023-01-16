using CompanyWebcast.Application.Common.DTOs;
using CompanyWebcast.Domain.WeatherForecast;

namespace CompanyWebcast.Application.Services
{
    public  interface IWeatherForecastService
    {
        public Task<WeatherForecastResponseDTO> AddWeatherForecast(AddWeatherForecastDTO weatherForecastDTO);
        public WeatherForecastResponseDTO UpdateWeatherForecast();
        public Task<List<WeatherForecastResponseDTO>> GetWeeklyWeatherForecast();
    }
}
