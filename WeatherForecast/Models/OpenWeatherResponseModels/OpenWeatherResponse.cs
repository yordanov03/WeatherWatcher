using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WeatherWatcher.Models
{
    public class OpenWeatherResponse
    {
        [JsonPropertyName("list")]
        public List<Forecast> Forecasts { get; set; }
    }

    public class Forecast
    {
        [JsonPropertyName("main")]
        public MainJsonData MainJsonData { get; set; }

        [JsonPropertyName("wind")]
        public WindData WindData { get; set; }

        [JsonPropertyName("weather")]
        public List<Weather> Weather { get; set; }

        [JsonPropertyName("dt_txt")]
        public string Date { get; set; }
    }

    public class MainJsonData
    {
        [JsonPropertyName("temp")]
        public decimal Temperature { get; set; }

        [JsonPropertyName("humidity")]
        public decimal Humidity { get; set; }
    }

    public class WindData
    {
        [JsonPropertyName("speed")]
        public decimal WindSpeed { get; set; }
    }

    public class Weather
    {
        [JsonPropertyName("description")]
        public string WeatherDescription { get; set; }
    }
}
