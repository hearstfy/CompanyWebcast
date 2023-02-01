using CompanyWebcast.Application.Common.Exceptions;
using CompanyWebcast.Application.Common.Interfaces.Persistance;
using CompanyWebcast.Application.Common.Requests;
using CompanyWebcast.Application.Mappings;
using CompanyWebcast.Application.Services;
using CompanyWebcast.Domain.WeatherForecast;
using CompanyWebcast.UnitTests.Fixtures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CompanyWebcast.UnitTests.Systems.Services
{
    public class WeatherForecastServiceTest
    {
        private Mock<IWeatherForecastRepository> _forecastRepository;
        private IWeatherForecastService _forecastService;

        public WeatherForecastServiceTest()
        {
            _forecastRepository = new Mock<IWeatherForecastRepository>();
        }

        [Fact]
        public async Task Given_WeatherForecastDoesNotExists_AddWeatherForecast_ShouldReturn_AddedWeatherForecast()
        {
            var addWeatherForecastRequest = WeatherForecastFixture.GetAddWeatherForecastRequest(DateTime.Now);
            var weatherForecast = addWeatherForecastRequest.ToAggregate();
            _forecastRepository.Setup(repo => repo.AddWeatherForecast(It.IsAny<WeatherForecast>())).ReturnsAsync(weatherForecast);
            _forecastRepository.Setup(repo => repo.GetWeatherForecastByDate(It.IsAny<DateOnly>())).Returns(Task.FromResult<WeatherForecast>(null));
            _forecastService = new WeatherForecastService(_forecastRepository.Object);

            var response = await _forecastService.AddWeatherForecast(addWeatherForecastRequest);

            Assert.Equal(response.Id, weatherForecast.Id.Value);
        }

        [Fact]
        public async Task Given_WeatherForecastExists_AddWeatherForecast_ShouldThrow_ForecastAlreadyExistsException()
        {
            var addWeatherForecastRequest = WeatherForecastFixture.GetAddWeatherForecastRequest(DateTime.Now);
            var weatherForecast = addWeatherForecastRequest.ToAggregate();
            _forecastRepository.Setup(repo => repo.GetWeatherForecastByDate(It.IsAny<DateOnly>())).ReturnsAsync(weatherForecast);
            _forecastService = new WeatherForecastService(_forecastRepository.Object);

            await Assert.ThrowsAsync<ForecastAlreadyExistsException>(() => _forecastService.AddWeatherForecast(addWeatherForecastRequest));
        }

        [Fact]
        public async Task Given_NoForecastExist_GetWeeklyForecast_ShouldReturn_EmptyList()
        {
            _forecastRepository.Setup(repo => repo.GetWeeklyWeatherForecast()).ReturnsAsync(new List<WeatherForecast>());
            _forecastService = new WeatherForecastService(_forecastRepository.Object);

            var result = await _forecastService.GetWeeklyWeatherForecast();

            Assert.NotNull(result);
            Assert.Empty(result.Data);
        }

        [Fact]
        public async Task GetWeeklyForecast_ShouldReturn_WeeklyForecast()
        {
            var weatherForecast = WeatherForecastFixture.GetWeatherForecasts(10);
            var now = DateOnly.FromDateTime(DateTime.Now);
            var weekLater = now.AddDays(7);
            _forecastRepository.Setup(repo => repo.GetWeeklyWeatherForecast()).ReturnsAsync(weatherForecast.Where(wf => wf.Date >= now && wf.Date < weekLater).ToList());
            _forecastService = new WeatherForecastService(_forecastRepository.Object);

            var result = await _forecastService.GetWeeklyWeatherForecast();

            Assert.NotNull(result);
            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task Given_WeatherForecastDoesNotExist_UpdateWeatherForecast_ShouldThrow_ForecastDoesNotExistsException()
        {
            _forecastRepository.Setup(repo => repo.GetWeatherForecastByDate(It.IsAny<DateOnly>())).Returns(Task.FromResult<WeatherForecast>(null));
            _forecastService = new WeatherForecastService(_forecastRepository.Object);
            var hourlyForecastList = WeatherForecastFixture.GetAddWeatherForecastHourlyRequests(5);
            var updateForecastRequest = new UpdateWeatherForecastRequest() { WeatherForecastId = Guid.NewGuid(), WeatherForecastHourlies = hourlyForecastList };

            await Assert.ThrowsAsync<ForecastDoesNotExistsException>(() => _forecastService.UpdateWeatherForecast(updateForecastRequest));
        }

        [Fact]
        public async Task Given_WeatherForecastExists_UpdateWeatherForecast_ShouldReturn_UpdatedWeatherForecast()
        {
            var weatherForecastHourlies = WeatherForecastFixture.GetWeatherForecastHourlies(1);
            var weatherForecast = WeatherForecastFixture.GetWeatherForecast(DateOnly.FromDateTime(DateTime.Now), weatherForecastHourlies);
   
            _forecastRepository.Setup(repo => repo.GetWeatherForecastById(It.IsAny<Guid>())).ReturnsAsync(weatherForecast);

            var hourlyForecastRequests = WeatherForecastFixture.GetAddWeatherForecastHourlyRequests(1);

            weatherForecastHourlies[0].SetTemperature((double)hourlyForecastRequests[0].TemperatureC);
            weatherForecast.UpdateHourlyForecasts(weatherForecastHourlies);

            _forecastRepository.Setup(repo => repo.UpdateWeatherForecast(It.IsAny<WeatherForecast>())).ReturnsAsync(weatherForecast);
            _forecastService = new WeatherForecastService(_forecastRepository.Object);
            var updateForecastRequest = new UpdateWeatherForecastRequest() { 
                WeatherForecastId = weatherForecast.Id.Value, 
                WeatherForecastHourlies = hourlyForecastRequests };

            var result = await _forecastService.UpdateWeatherForecast(updateForecastRequest);

            Assert.Equal(weatherForecast.Id.Value, result.Id);
            Assert.Equal(hourlyForecastRequests[0].TemperatureC, result.HourlyForecasts[0].TemperatureInCelcius);
        }

    }
}
