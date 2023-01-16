using CompanyWebcast.Application.Common.Validations;
using System.ComponentModel.DataAnnotations;

namespace CompanyWebcast.Application.Common.DTOs
{
    public class WeatherForecastHourlyDTO
    {
        [Required]
        [Range(0,23)]
        public int StartHour { get; set; }
        [Required]
        [Range(0,23)]
        [CompareHours(nameof(StartHour))]
        public int EndHour { get; set; }
        [Required]
        [Range(-60.0, 60.0)]
        public double TempratureC { get; set; }

    }
}
