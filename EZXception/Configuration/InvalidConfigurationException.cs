using System;

namespace EZXception.Configuration
{
    /// <summary>
    /// Thrown when a configuration value is present but invalid (wrong type, out of range, malformed).
    /// </summary>
    public class InvalidConfigurationException : ConfigurationException
    {
        public object? ActualValue { get; }
        public string? ExpectedDescription { get; }

        public InvalidConfigurationException(string configKey, object? actualValue = null, string? expectedDescription = null)
            : base(BuildMessage(configKey, actualValue, expectedDescription), configKey)
        {
            ActualValue = actualValue;
            ExpectedDescription = expectedDescription;
        }

        public InvalidConfigurationException(string configKey, string message, Exception innerException)
            : base(message, innerException, configKey) { }

        private static string BuildMessage(string key, object? actual, string? expected)
        {
            if (actual != null && expected != null)
                return $"Configuration key '{key}' has an invalid value '{actual}'. Expected: {expected}.";
            if (actual != null)
                return $"Configuration key '{key}' has an invalid value: '{actual}'.";
            return $"Configuration key '{key}' has an invalid value.";
        }
    }
}
