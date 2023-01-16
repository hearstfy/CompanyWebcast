using CompanyWebcast.Domain.Common.Models;

namespace CompanyWebcast.Domain.WeatherForecast.ValueObjects
{
    public sealed class WeatherForecastId : ValueObject
    {
        public Guid Value { get; }

        private WeatherForecastId(Guid value)
        {
            Value = value;
        }

        public static WeatherForecastId Create(Guid value)
        {
            return new WeatherForecastId(value);
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
