using CompanyWebcast.Domain.Common.Models;
using CompanyWebcast.Domain.WeatherForecast.ValueObjects;

namespace CompanyWebcast.Domain.WeatherForecast.Entities
{
    public sealed class WeatherForecastHourly : Entity<WeatherForecastHourlyId>
    {
        public int StartHour { get; private set; }
        public int EndHour { get; private set; }

        public double TemperatureC { get; private set; }
        public double TemperatureF
        {
            get
            {
                return 32 + (double)(TemperatureC / 0.5556);
            }
        }
        public string Summary
        {
            get
            {
                return TemperatureC switch
                {
                    <= 60 and >=55.1 => "Scorching",
                    <= 50 and >= 40.1 => "Sweltering",
                    <= 40 and >= 30.1 => "Hot",
                    <= 30 and >= 25.1 => "Balmy",
                    <= 25 and >= 20.1 => "Warm",
                    <= 20 and >= 15.1 => "Mild",
                    <= 15 and >= 10.1 => "Cool",
                    <= 10 and >= 5.1 => "Chilly",
                    <= 5 and >= -10.9 => "Bracing",
                    <= -11 and >= -60 => "Freezing"

                };
            }
        }

        private WeatherForecastHourly(WeatherForecastHourlyId weatherForecastHourlyId, int startHour, int endHour, double temprature)
            : base(weatherForecastHourlyId)
        {
            StartHour = startHour;
            EndHour = endHour;
            TemperatureC = temprature;
        }

        public static WeatherForecastHourly Create(int startHour, int endHour, double temprature)
        {
            return new(WeatherForecastHourlyId.Create(Guid.NewGuid()), startHour, endHour, temprature);
        }

    #pragma warning disable CS8618
        private WeatherForecastHourly() { }

    #pragma warning restore CS8618
    }
}
