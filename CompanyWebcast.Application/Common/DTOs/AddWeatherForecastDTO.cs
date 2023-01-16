using CompanyWebcast.Application.Common.Validations;
using System.ComponentModel.DataAnnotations;

namespace CompanyWebcast.Application.Common.DTOs
{
    public record AddWeatherForecastDTO
    {
        [Required]
        [NowOrLater(nameof(WeatherForecastsHourly))]
        public DateTime? Date { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "At least one hourly forecast is needed.")]
        [MaxLength(24, ErrorMessage ="Maximum 24 hourly forecasts is allowed")]
        public List<AddWeatherForecastHourlyDTO> WeatherForecastsHourly { get; set;}
    }
}
