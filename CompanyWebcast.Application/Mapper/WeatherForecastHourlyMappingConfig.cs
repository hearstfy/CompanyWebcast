using CompanyWebcast.Application.Common.DTOs;
using CompanyWebcast.Domain.WeatherForecast.Entities;
using Mapster;

namespace CompanyWebcast.Application.Mapper
{
    public class WeatherForecastHourlyMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<WeatherForecastHourlyDTO, WeatherForecastHourly>()
                .Map(dest => dest.Id, src => Guid.NewGuid());
        }
    }
}
