using FakeItEasy;
using System;
using WeatherWatcher.Models;

namespace WeatherWatcher.Tests.Fakes
{
    public class WeatherForecastFake
    {
       public class WeatherForecastDummyFactory : IDummyFactory
        {
            public Priority Priority => Priority.Default;

            public bool CanCreate(Type type) => type == typeof(WeatherForecast);


            public object Create(Type type) =>
                new WeatherForecast(
                        "Bonn",
                        DateTime.Now.ToShortDateString(),
                        12,
                        86,
                        2,
                        "cloudy");
        }


    }
}
