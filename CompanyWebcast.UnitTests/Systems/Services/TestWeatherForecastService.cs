using CompanyWebcast.Application.Common.DTOs;
using CompanyWebcast.Application.Common.Exceptions;
using CompanyWebcast.Application.Common.Interfaces.Persistance;
using CompanyWebcast.Application.Services;
using CompanyWebcast.Infrastructure.Persistance;
using CompanyWebcast.Infrastructure.Persistance.Repositories;
using CompanyWebcast.UnitTests.Fixtures;
using CompanyWebcast.UnitTests.Helpers.DataProviders;
using CompanyWebcast.UnitTests.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CompanyWebcast.UnitTests.Systems.Services
{
    [Collection("Database Collection")]
    public class TestWeatherForecastService
    {
        ApplicationDBContext dbContext;
        IWeatherForecastRepository weatherForecastRepository;
        IWeatherForecastService weatherForecastService;
        public TestWeatherForecastService(DatabaseFixture fixture)
        {
            dbContext = fixture.GetDbContext();
            weatherForecastRepository = new WeatherForecastRepository(dbContext);
            weatherForecastService = new WeatherForecastService(weatherForecastRepository);
        }

        [Fact]
        public async void GivenSevenOrMoreForecastsExist_GetWeeklyForecasts_ShouldReturn_WeeklyForecastStartingFromToday()
        {
            dbContext.DeleteAll();
            dbContext.Populate(10);

            var result = await weatherForecastService.GetWeeklyWeatherForecast();
            Assert.True(result.Count == 7);
            Assert.True(result.First().Date == DateOnly.FromDateTime(DateTime.Now));
            Assert.True(result.Last().Date == DateOnly.FromDateTime(DateTime.Now.AddDays(6)));
        }

        [Fact]
        public async void GivenEqualOrLessThanSevenForecastsExist_GetWeeklyForecasts_ShouldReturn_AllWeeklyForecastStartingFromToday()
        {
            dbContext.DeleteAll();
            dbContext.Populate(6);

            var result = await weatherForecastService.GetWeeklyWeatherForecast();
            var allForecastsCount = dbContext.WeatherForecasts.Count(wf => wf.Date >= DateOnly.FromDateTime(DateTime.Now));

            Assert.True(result.First().Date == DateOnly.FromDateTime(DateTime.Now));
            Assert.True(result.Count == allForecastsCount);

        }

        [Fact]
        public async void GivenNoForecastExistsForToday_AddForecastForToday_ShouldAdd_TodaysForecast()
        {
            dbContext.DeleteAll();
            var addWeatherForecastDTO = WeatherForecastDataProvider.GetAddWeatherForecastDTO(DateTime.Now);

            var result = await weatherForecastService.AddWeatherForecast(addWeatherForecastDTO);

            Assert.NotNull(result);
            Assert.True(result.Date == DateOnly.FromDateTime(DateTime.Now));
        }

        [Fact]
        public async void GivenForecastExistsForToday_AddForecastForToday_ShouldThrow_ForecastAlreadyExistsException()
        {
            dbContext.DeleteAll();
            dbContext.Populate(1);
            var addWeatherForecastDTO = WeatherForecastDataProvider.GetAddWeatherForecastDTO(DateTime.Now);

            Func<Task> action = async () => await weatherForecastService.AddWeatherForecast(addWeatherForecastDTO);
            var ex = await Assert.ThrowsAsync<ForecastAlreadyExistsException>(action);

            Assert.True(ex.GetType() == typeof(ForecastAlreadyExistsException));
        }

        [Fact]
        public async void GivenNoForecastsExist_UpdateForecast_ShouldThrow_ForecastDoesNotExistsException()
        {
            dbContext.DeleteAll();
            dbContext.Populate(5);
            var forecastId = Guid.NewGuid();
            var hourlyForecastDTO = WeatherForecastDataProvider.GetAddWeatherForecastHourlyDTO(10, 11, 20.5);

            Func<Task> action = async () => await weatherForecastService.UpdateWeatherForecast(forecastId, new List<AddWeatherForecastHourlyDTO>() { hourlyForecastDTO });
            var ex = await Assert.ThrowsAsync<ForecastDoesNotExistsException>(action);

            Assert.True(ex.GetType() == typeof(ForecastDoesNotExistsException));
        }
        [Fact]
        public async void GivenForecastsExist_UpdateForecast_Should_AddNewHourlyForecast()
        {
            dbContext.DeleteAll();
            var forecastHourly = WeatherForecastDataProvider.GetWeatherForecastHourly(10, 11, 20.5);
            var forecast = WeatherForecastDataProvider.GetWeatherForecast(DateOnly.FromDateTime(DateTime.Now), new List<Domain.WeatherForecast.Entities.WeatherForecastHourly>() { forecastHourly });
            dbContext.Add(forecast);
            dbContext.SaveChanges();

            var existingForecast = dbContext.WeatherForecasts.First();
            var hourlyForecastDTO = WeatherForecastDataProvider.GetAddWeatherForecastHourlyDTO(11, 12, 40.85);

            var result = await weatherForecastService.UpdateWeatherForecast(existingForecast.Id.Value, new List<AddWeatherForecastHourlyDTO>() { hourlyForecastDTO });

            Assert.True(result.HourlyForecasts.Count == 2);
        }

        [Fact]
        public async void GivenForecastsExist_UpdateForecast_Should_UpdateExistingHourlyForecast()
        {
            dbContext.DeleteAll();
            var forecastHourly = WeatherForecastDataProvider.GetWeatherForecastHourly(10, 11, 20.5);
            var forecast = WeatherForecastDataProvider.GetWeatherForecast(DateOnly.FromDateTime(DateTime.Now), new List<Domain.WeatherForecast.Entities.WeatherForecastHourly>() { forecastHourly });
            dbContext.Populate(forecast);

            var existingForecast = dbContext.WeatherForecasts.First();
            var hourlyForecastDTO = WeatherForecastDataProvider.GetAddWeatherForecastHourlyDTO(10, 11, 40.85);

            var result = await weatherForecastService.UpdateWeatherForecast(existingForecast.Id.Value, new List<AddWeatherForecastHourlyDTO>() { hourlyForecastDTO });

            Assert.True(result.HourlyForecasts.First().TemperatureInCelcius == 40.85);
        }

    }
}
