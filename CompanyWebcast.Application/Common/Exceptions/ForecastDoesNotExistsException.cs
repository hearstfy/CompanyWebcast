namespace CompanyWebcast.Application.Common.Exceptions
{
    public class ForecastDoesNotExistsException : Exception
    {
        public ForecastDoesNotExistsException(string message) : base(message)
        {
        }
    }
}
