namespace WeatherWatcher.Models
{
    public class OpenWeatherApiOptions
    {
        public const string AppOptions = "parameters";

        public string ApiKey { get; set; }
        public string ApiUrl { get; set; }
    }
}
