using CompanyWebcast.Domain.WeatherForecast;

namespace CompanyWebcast.Application.Common.Interfaces.Persistance
{
    public interface IWeatherForecastRepository
    {
        public Task<WeatherForecast> AddWeatherForecast(WeatherForecast weatherForecast);
        public WeatherForecast GetWeatherForecastByDate(DateOnly date);
        public Task<WeatherForecast> GetWeatherForecastById(Guid id);
        public Task<WeatherForecast> UpdateWeatherForecast(WeatherForecast weatherForecast);
        public Task<List<WeatherForecast>> GetWeeklyWeatherForecast();
    }
}
