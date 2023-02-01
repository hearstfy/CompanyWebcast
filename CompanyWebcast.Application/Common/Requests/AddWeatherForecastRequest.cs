namespace CompanyWebcast.Application.Common.Requests
{
    public class AddWeatherForecastRequest
    {
        public DateOnly Date { get; set; }
        public List<AddUpdateWeatherForecastHourlyRequest> Hourlies { get; set; }
    }
}
