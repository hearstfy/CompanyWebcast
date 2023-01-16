using CompanyWebcast.Application.Common.DTOs;
using CompanyWebcast.Application.Common.Exceptions;
using CompanyWebcast.Application.Common.Interfaces.Persistance;
using CompanyWebcast.Domain.WeatherForecast;
using CompanyWebcast.Domain.WeatherForecast.Entities;
using MapsterMapper;

namespace CompanyWebcast.Application.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IWeatherForecastRepository _forecastRepository;
        private readonly IMapper _mapper;

        public WeatherForecastService(IMapper mapper, IWeatherForecastRepository forecastRepository)
        {
            _mapper = mapper;
            _forecastRepository = forecastRepository;
        }

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        public async Task<WeatherForecast> AddWeatherForecast(WeatherForecastDTO weatherForecastDTO)
        {
            var existingForecast = _forecastRepository.GetWeatherForecastByDate(DateOnly.FromDateTime(weatherForecastDTO.Date.GetValueOrDefault(DateTime.Now)));
            if(existingForecast is not null)
            {
                throw new ForecastAlreadyExistsException($"WeatherForecast for Day {weatherForecastDTO.Date?.ToString("dd-MM-yyyy")} already exists. You can only update it.", 409);
            }

            var weatherForecast = WeatherForecast.Create(
                date: DateOnly.FromDateTime(weatherForecastDTO.Date.GetValueOrDefault(DateTime.Now)), 
                weatherForecastHourlies: weatherForecastDTO.WeatherForecastsHourly
                .ConvertAll(hourly => WeatherForecastHourly.Create(
                    startHour: hourly.StartHour, 
                    endHour: hourly.EndHour, 
                    temprature: hourly.TempratureC)));

            var newForecast = await _forecastRepository.AddWeatherForecast(weatherForecast);
            return newForecast;
        }

        public async Task<List<WeatherForecast>> GetWeeklyWeatherForecast()
        {
            
            return await _forecastRepository.GetWeeklyWeatherForecast();
        }

        public WeatherForecastResult UpdateWeatherForecast()
        {
            throw new NotImplementedException();
        }
    }
}
