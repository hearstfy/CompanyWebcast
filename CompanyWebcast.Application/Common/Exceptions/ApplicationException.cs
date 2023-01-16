namespace CompanyWebcast.Application.Common.Exceptions
{
    public class ApplicationException : Exception
    {
        public int StatusCode { get; set; } = 500;
        public ApplicationException(string? message, int? statusCode) : base(message)
        {
            if(statusCode != null)
                StatusCode = (int)statusCode;
        }
    }
}
