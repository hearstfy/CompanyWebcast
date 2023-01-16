namespace CompanyWebcast.Application.Common.DTOs
{
    public record WeatherForecastResponseDTO
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public List<WeatherForecastHourlyResponseDTO> HourlyForecasts { get; set; }

    }
}
