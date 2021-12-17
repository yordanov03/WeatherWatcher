using System;

namespace WeatherWatcher.Api.Exceptions
{
    public class CalculationException : Exception
    {
        public CalculationException(string message) : base(message)
        {

        }

    }
}
