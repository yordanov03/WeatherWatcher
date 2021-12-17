using FakeItEasy;
using System;
using System.Collections.Generic;
using WeatherWatcher.Models;

namespace WeatherWatcher.Tests.Fakes
{
    public class WeatherforecastsFake : IDummyFactory
    {
        public Priority Priority => Priority.Default;

        public bool CanCreate(Type type) => type == typeof(List<WeatherForecast>);


        public object Create(Type type)
        {
            var forecasts = new List<WeatherForecast>();
            forecasts.Add(A.Dummy<WeatherForecast>());
            forecasts.Add(A.Dummy<WeatherForecast>());

            return forecasts;
        }
    }
}
