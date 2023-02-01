using CompanyWebcast.Application.Common.Exceptions;
using CompanyWebcast.Application.Common.Interfaces.Persistance;
using CompanyWebcast.Application.Common.Requests;
using CompanyWebcast.Application.Common.Responses;
using CompanyWebcast.Application.Mappings;

namespace CompanyWebcast.Application.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IWeatherForecastRepository _forecastRepository;

        public WeatherForecastService(IWeatherForecastRepository forecastRepository)
        {
            _forecastRepository = forecastRepository;
        }

        public async Task<AddWeatherForecastResponse> AddWeatherForecast(AddWeatherForecastRequest request)
        {
            var existingForecast = await _forecastRepository.GetWeatherForecastByDate(request.Date);
            if (existingForecast is not null)
            {
                throw new ForecastAlreadyExistsException($"Weather forecast for Day {request.Date.ToString("dd-MM-yyyy")} already exists. You can only update it.");
            }

            var weatherForecast = request.ToAggregate();

            var newForecast = await _forecastRepository.AddWeatherForecast(weatherForecast);
            return newForecast.ToResponse();
        }

        public async Task<GetWeatherForecastWeeklyResponse> GetWeeklyWeatherForecast()
        {
            var weeklyForecast = await _forecastRepository.GetWeeklyWeatherForecast();
            return new GetWeatherForecastWeeklyResponse()
            {
                Data = weeklyForecast.ConvertAll(wf => wf.ToResponse())
            };
    }

    public async Task<AddWeatherForecastResponse> UpdateWeatherForecast(Guid id, List<AddUpdateWeatherForecastHourlyRequest> request)
    {
        var existingForecast = await _forecastRepository.GetWeatherForecastById(id);
        if (existingForecast == null)
        {
            throw new ForecastDoesNotExistsException($"Weather forecast with Id {id} does not exist.");
        }

        var forecastHourlies = request.ConvertAll(fh => fh.ToEntity());
        existingForecast.UpdateHourlyForecasts(forecastHourlies);

        var updatedForecast = await _forecastRepository.UpdateWeatherForecast(existingForecast);

        return updatedForecast.ToResponse();
    }
}
}
