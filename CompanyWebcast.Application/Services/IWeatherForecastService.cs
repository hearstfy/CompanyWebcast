using CompanyWebcast.Application.Common.DTOs;
using CompanyWebcast.Domain.WeatherForecast;

namespace CompanyWebcast.Application.Services
{
    public  interface IWeatherForecastService
    {
        public Task<WeatherForecastResponseDTO> AddWeatherForecast(AddWeatherForecastDTO weatherForecastDTO);
        public Task<WeatherForecastResponseDTO> UpdateWeatherForecast(Guid id, List<AddWeatherForecastHourlyDTO> forecastHourlyDTOs);
        public Task<List<WeatherForecastResponseDTO>> GetWeeklyWeatherForecast();
    }
}
