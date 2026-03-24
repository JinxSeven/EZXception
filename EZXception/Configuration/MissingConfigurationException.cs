using System;

namespace EZXception.Configuration
{
    /// <summary>
    /// Thrown when a required configuration value or section is absent.
    /// </summary>
    public class MissingConfigurationException : ConfigurationException
    {
        public MissingConfigurationException(string configKey)
            : base($"Required configuration key '{configKey}' is missing.", configKey) { }

        public MissingConfigurationException(string configKey, string message)
            : base(message, configKey) { }

        public MissingConfigurationException(string configKey, string message, Exception innerException)
            : base(message, innerException, configKey) { }
    }
}
