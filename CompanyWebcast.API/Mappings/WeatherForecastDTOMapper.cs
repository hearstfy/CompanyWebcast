using CompanyWebcast.API.DTOs;
using CompanyWebcast.Application.Common.Requests;

namespace CompanyWebcast.API.Mappings
{
    public static class WeatherForecastDTOMapper
    {
        public static AddWeatherForecastRequest ToRequest(this AddWeatherForecastDTO dto)
        {
            return new AddWeatherForecastRequest()
            {
                Date = DateOnly.FromDateTime((DateTime)dto.Date),
                Hourlies = dto.WeatherForecastsHourly.ConvertAll(wfh => wfh.ToRequest())
            };
        }

        public static AddUpdateWeatherForecastHourlyRequest ToRequest(this AddWeatherForecastHourlyDTO dto)
        {
            return new AddUpdateWeatherForecastHourlyRequest()
            {
                StartHour = dto.StartHour,
                EndHour = dto.EndHour,
                TemperatureC = (double)dto.TemperatureC,
            };
        }
    }
}
