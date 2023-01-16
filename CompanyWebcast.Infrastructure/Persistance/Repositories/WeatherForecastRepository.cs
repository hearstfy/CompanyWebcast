using CompanyWebcast.Application.Common.Interfaces.Persistance;
using CompanyWebcast.Domain.WeatherForecast;
using CompanyWebcast.Domain.WeatherForecast.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CompanyWebcast.Infrastructure.Persistance.Repositories
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public WeatherForecastRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<WeatherForecast> AddWeatherForecast(WeatherForecast weatherForecast)
        {
            var newForecast = _dbContext.Add(weatherForecast).Entity;
            await _dbContext.SaveChangesAsync();
            return newForecast;
        }

        public WeatherForecast GetWeatherForecastByDate(DateOnly date)
        {
            var forecast = _dbContext.WeatherForecasts.FirstOrDefault(x => x.Date == date);
            return forecast;
        }
        public async Task<WeatherForecast> GetWeatherForecastById(Guid id)
        {
            var forecast = await _dbContext.WeatherForecasts.FindAsync(WeatherForecastId.Create(id));
            return forecast;
        }


        public async Task<List<WeatherForecast>> GetWeeklyWeatherForecast()
        {
            var now = DateOnly.FromDateTime(DateTime.Now);
            var weekLater = now.AddDays(7);

            var weeklyForecast = _dbContext.WeatherForecasts.Where(wf => wf.Date >= now && wf.Date < weekLater).OrderBy(wf => wf.Date).ToList();
            return weeklyForecast;
        }

        public async Task<WeatherForecast> UpdateWeatherForecast(WeatherForecast weatherForecast)
        {
            var updatedForecast = _dbContext.Update(weatherForecast).Entity;
            await _dbContext.SaveChangesAsync();
            return updatedForecast;

        }
    }
}
