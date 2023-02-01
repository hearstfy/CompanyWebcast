namespace CompanyWebcast.Application.Common.Responses
{
    public class AddWeatherForecastResponse
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public List<AddUpdateWeatherForecastHourlyResponse> HourlyForecasts { get; set; }
    }
}
