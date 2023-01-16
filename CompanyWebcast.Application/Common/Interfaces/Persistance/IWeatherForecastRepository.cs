using CompanyWebcast.Domain.WeatherForecast;

namespace CompanyWebcast.Application.Common.Interfaces.Persistance
{
    public interface IWeatherForecastRepository
    {
        public Task<WeatherForecast> AddWeatherForecast(WeatherForecast weatherForecast);
        public WeatherForecast GetWeatherForecastByDate(DateOnly date);
        public Task<WeatherForecast> UpdateWeatherForecast(int weatherForecastId, WeatherForecast weatherForecast);
        public Task<List<WeatherForecast>> GetWeeklyWeatherForecast();
    }
}
