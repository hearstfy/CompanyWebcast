using CompanyWebcast.Domain.Common.Models;

namespace CompanyWebcast.Domain.WeatherForecast.ValueObjects
{
    public sealed class WeatherForecastHourlyId : ValueObject
    {
        public Guid Value { get; }

        private WeatherForecastHourlyId(Guid value)
        {
            Value = value;
        }

        public static WeatherForecastHourlyId Create(Guid value)
        {
            return new(value);
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
