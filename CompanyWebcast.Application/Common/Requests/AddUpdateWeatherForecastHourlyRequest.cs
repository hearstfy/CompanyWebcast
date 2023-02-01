namespace CompanyWebcast.Application.Common.Requests
{
    public class AddUpdateWeatherForecastHourlyRequest
    {
        public int StartHour { get; set; }
        public int EndHour { get; set; }
        public double TemperatureC { get; set; }
    }
}
