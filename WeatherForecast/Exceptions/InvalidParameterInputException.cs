using System;

namespace WeatherWatcher.Api.Exceptions
{
    public class InvalidParameterInputException : Exception
    {
        public InvalidParameterInputException(string message) : base(message)
        {
        }
    }
}
