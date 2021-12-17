using System;
using System.Net;

namespace WeatherWatcher.Api.Exceptions
{
    public class DeserializeException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public DeserializeException(HttpStatusCode statusCode, string message) : base(message)
            => StatusCode = statusCode;
    }
}
