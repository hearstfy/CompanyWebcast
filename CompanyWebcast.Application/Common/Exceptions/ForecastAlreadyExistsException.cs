namespace CompanyWebcast.Application.Common.Exceptions
{
    public class ForecastAlreadyExistsException : Exception
    {
        public ForecastAlreadyExistsException(string message) : base(message)
        {
        }
    }
}
