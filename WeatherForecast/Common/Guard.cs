using System;
using System.Text.RegularExpressions;
using static WeatherWatcher.Api.Common.Constants;

namespace WeatherWatcher.Api.Common
{
    public static class Guard
    {
        public static void AgainstEmptyString<InvalidParameterInputException>(string value, string name = "Value")
        {
            if (!string.IsNullOrEmpty(value))
            {
                return;
            }

            ThrowException<InvalidParameterInputException>($"{name} cannot be null ot empty.");
        }

        public static void ForStringLength<InvalidParameterInputException>(string value, int minLength, int maxLength, string name = "Value")
        {
            AgainstEmptyString<InvalidParameterInputException>(value, name);

            if (minLength <= value.Length && value.Length <= maxLength)
            {
                return;
            }

            ThrowException<InvalidParameterInputException>($"{name} must have between {minLength} and {maxLength} symbols.");
        }

        public static void ForValidZipcode<InvalidParameterInputException>(string zipcode, string name = "Value")
        {
            bool validZipcode;

            if (!string.IsNullOrEmpty(zipcode))
            {
                validZipcode = Regex.IsMatch(zipcode, zipcodeRegex);

                if (validZipcode)
                {
                    return;
                }
            }

            ThrowException<InvalidParameterInputException>($"{name} must be valid German zipcode.");
        }

        private static void ThrowException<InvalidParameterInputException>(string message)
        {
            var exception = new Exception(message);

            throw exception;
        }
    }
}
