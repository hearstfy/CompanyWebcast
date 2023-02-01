using CompanyWebcast.Application.Common.Validations;
using System.ComponentModel.DataAnnotations;

namespace CompanyWebcast.API.DTOs
{
    public record AddWeatherForecastHourlyDTO
    {
        [Required]
        [Range(0, 23)]
        public int StartHour { get; set; }
        [Required]
        [Range(0, 23)]
        [CompareHours(nameof(StartHour))]
        public int EndHour { get; set; }
        [Required]
        [Range(-60.0, 60.0)]
        public double? TemperatureC { get; set; }

    }
}
