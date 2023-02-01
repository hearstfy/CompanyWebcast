using CompanyWebcast.Application.Common.Requests;
using CompanyWebcast.Application.Common.Responses;

namespace CompanyWebcast.Application.Services
{
    public  interface IWeatherForecastService
    {
        public Task<AddWeatherForecastResponse> AddWeatherForecast(AddWeatherForecastRequest request);
        public Task<AddWeatherForecastResponse> UpdateWeatherForecast(Guid id, List<AddUpdateWeatherForecastHourlyRequest> request);
        public Task<GetWeatherForecastWeeklyResponse> GetWeeklyWeatherForecast();
    }
}
