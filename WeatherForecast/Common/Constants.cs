namespace WeatherWatcher.Api.Common
{
    public static class Constants
    {
        public const string CountryCode = "de";
        public const string ZipcodeRegex = @"^\d{5}$";
        public const int MinLocationLength = 3;
        public const int MaxLocationLength = 25;

        public static class WeatherDescriptionConstants
        {
            public const string Sunny = "sun";
            public const string Rainy = "rain";
            public const string Snowy = "snow";
            public const string Clear = "clear";
            public const string Cloudy = "cloud";
        }

    }

}
