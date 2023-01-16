using CompanyWebcast.Application.Common.DTOs;
using System.ComponentModel.DataAnnotations;

namespace CompanyWebcast.Application.Common.Validations
{
    public class NowOrLater : ValidationAttribute
    {
        private readonly string _hourlyForecastsProperty;

        public NowOrLater(string hourlyForecastsProperty)
        {
            _hourlyForecastsProperty = hourlyForecastsProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var providedForecastsProperty = validationContext.ObjectType.GetProperty(_hourlyForecastsProperty);
            var providedForecasts = (List<WeatherForecastHourlyDTO>)providedForecastsProperty.GetValue(validationContext.ObjectInstance);
            var providedDate = ((DateTime)value).Day;
            var now= DateTime.Now;

            if(providedDate == now.Day && providedForecasts.Any(f => f.StartHour < now.Hour))
            {
                return new ValidationResult("You can only add hourly forecasts for current hour and later");
            }

            return ValidationResult.Success;
        }
    }

    //public class CurrentHourOrLater : ValidationAttribute
    //{
    //    private readonly string _dateProperty;

    //    public CurrentHourOrLater(string dateProperty)
    //    {
    //        _dateProperty = dateProperty;
    //    }

    //    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    //    {
    //        var providedDateProperty = validationContext.ObjectType.GetProperty(_dateProperty);
    //        var providedDate = DateOnly.FromDateTime((DateTime)providedDateProperty.GetValue(validationContext.ObjectInstance));
    //        var providedHour = (int)value;

    //        if(providedDate == DateOnly.FromDateTime(DateTime.Now) && providedHour >= DateTime.Now.Hour)
    //        {
    //            return ValidationResult.Success;
    //        }
    //        return new ValidationResult("You can only add hourly forecasts for current hour and later");
    //    }
    //}

    public class CompareHours: ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public CompareHours(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var endHour = (int)value;
            var startHourProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);
            var startHour = (int)startHourProperty.GetValue(validationContext.ObjectInstance);

            if(startHour != 23 && startHour >= endHour)
            {
                return new ValidationResult("EndHour must be later than StartHour");
            }

            if((endHour != 0 && endHour - startHour != 1) || (endHour == 0 && endHour - startHour != -23))
            {
                return new ValidationResult("Difference between EndHour and StartHour must be 1 Hour.");
            }

            return ValidationResult.Success;
        }
    }
}
