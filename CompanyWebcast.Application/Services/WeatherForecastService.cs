using CompanyWebcast.Application.Common.DTOs;
using CompanyWebcast.Application.Common.Exceptions;
using CompanyWebcast.Application.Common.Interfaces.Persistance;
using CompanyWebcast.Application.Mapper;

namespace CompanyWebcast.Application.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IWeatherForecastRepository _forecastRepository;

        public WeatherForecastService(IWeatherForecastRepository forecastRepository)
        {
            _forecastRepository = forecastRepository;
        }

        public async Task<WeatherForecastResponseDTO> AddWeatherForecast(AddWeatherForecastDTO weatherForecastDTO)
        {
            var existingForecast = _forecastRepository.GetWeatherForecastByDate(DateOnly.FromDateTime(weatherForecastDTO.Date.GetValueOrDefault(DateTime.Now)));
            if(existingForecast is not null)
            {
                throw new ForecastAlreadyExistsException($"Weather forecast for Day {weatherForecastDTO.Date?.ToString("dd-MM-yyyy")} already exists. You can only update it.", 409);
            }

            var weatherForecast = weatherForecastDTO.ToAggregate();

            var newForecast = (await _forecastRepository.AddWeatherForecast(weatherForecast)).ToDTO();
            return newForecast;
        }

        public async Task<List<WeatherForecastResponseDTO>> GetWeeklyWeatherForecast()
        {
            var weeklyForecast = await _forecastRepository.GetWeeklyWeatherForecast();
            return weeklyForecast.ConvertAll(wf => wf.ToDTO());
        }

        public async Task<WeatherForecastResponseDTO> UpdateWeatherForecast(Guid id, List<AddWeatherForecastHourlyDTO> forecastHourlyDTOs)
        {
            var existingForecast = await _forecastRepository.GetWeatherForecastById(id);
            if(existingForecast == null)
            {
                throw new ForecastDoesNotExistsException($"Weather forecast with Id {id} does not exist.", 404);
            }

            var forecastHourlies = forecastHourlyDTOs.ConvertAll(fh => fh.ToEntity());
            forecastHourlies.AddRange(existingForecast.HourlyForecasts);
            existingForecast.UpdateHourlyForecasts(forecastHourlies.GroupBy(forecastHourlies => forecastHourlies.StartHour).Select(fhg => fhg.First()).ToList());

            var updatedForecast = await _forecastRepository.UpdateWeatherForecast(existingForecast);

            return updatedForecast.ToDTO();
        }
    }
}
