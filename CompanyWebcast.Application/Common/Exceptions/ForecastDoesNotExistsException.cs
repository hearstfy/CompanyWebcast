namespace CompanyWebcast.Application.Common.Exceptions
{
    public class ForecastDoesNotExistsException : ApplicationException
    {
        public ForecastDoesNotExistsException(string? message, int? statusCode) : base(message, statusCode)
        {
        }
    }
}
