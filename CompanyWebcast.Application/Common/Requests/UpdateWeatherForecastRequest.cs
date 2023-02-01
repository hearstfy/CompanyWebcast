using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWebcast.Application.Common.Requests
{
    public class UpdateWeatherForecastRequest
    {
        public Guid WeatherForecastId { get; set; }
        public List<AddUpdateWeatherForecastHourlyRequest> WeatherForecastHourlies { get; set; }
    }
}
