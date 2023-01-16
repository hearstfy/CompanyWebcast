using CompanyWebcast.API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace CompanyWebcast.UnitTests.Systems.Controllers
{
    public class TestWeatherForecastController
    {
        [Fact]
        public async Task Get_OnSuccess_ReturnSatusCode200Async()
        {
            var sut = new WeatherForecastController();

            var res = (OkObjectResult) await sut.Get();

            res.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Get_OnSuccess_Invokes_ForecastService()
        {
            var mockForecastService = Mock<IForecastService>();
            var sut = new WeatherForecastController();
        }
    }
}
