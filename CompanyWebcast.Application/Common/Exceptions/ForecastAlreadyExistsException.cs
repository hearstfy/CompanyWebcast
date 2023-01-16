using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWebcast.Application.Common.Exceptions
{
    public class ForecastAlreadyExistsException : ApplicationException
    {
        public ForecastAlreadyExistsException(string? message, int? statusCode) : base(message, statusCode)
        {
        }
    }
}
