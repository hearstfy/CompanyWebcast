namespace CompanyWebcast.Application.Common.Responses
{
    public class AddUpdateWeatherForecastHourlyResponse
    {
        public int StartHour { get; set; }
        public int EndHour { get; set; }
        public double TemperatureInCelcius { get; set; }
        public double TemperatureInFahrenheit { get; set; }
        public string Summary { get; set; }
    }
}
