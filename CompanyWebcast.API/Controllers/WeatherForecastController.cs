using CompanyWebcast.Application.Common.DTOs;
using CompanyWebcast.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyWebcast.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _forecastService;

        public WeatherForecastController(IWeatherForecastService forecastService)
        {
            _forecastService = forecastService;
        }

        [HttpPost()]
        public async Task<IActionResult> AddForecast([FromBody]AddWeatherForecastDTO forecastDTO)
        {
            var newForecast = await _forecastService.AddWeatherForecast(forecastDTO);
            return Created(nameof(AddForecast), newForecast);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateForecast([FromRoute] Guid id, [FromBody]List<AddWeatherForecastHourlyDTO> forecastHourlyDTOs)
        {
            var updatedForecast = await _forecastService.UpdateWeatherForecast(id, forecastHourlyDTOs);
            return Ok(updatedForecast);
        }

        [HttpGet("weekly")]
        public async Task<IActionResult> GetWeeklyForecast()
        {
            var result = await _forecastService.GetWeeklyWeatherForecast();
            return Ok(result);
        }
    }
}
