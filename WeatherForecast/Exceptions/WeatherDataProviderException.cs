using System;
using System.Net;

namespace WeatherWatcher.Api.Exceptions
{
    public class WeatherDataProviderException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public WeatherDataProviderException(HttpStatusCode statusCode, string message) : base(message)
            => StatusCode = statusCode;
    }
}
