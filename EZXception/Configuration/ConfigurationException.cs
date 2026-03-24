using System;

namespace EZXception.Configuration
{
    /// <summary>
    /// Base exception for all configuration-related errors.
    /// </summary>
    public class ConfigurationException : Exception
    {
        public string? ConfigKey { get; }

        public ConfigurationException(string message, string? configKey = null)
            : base(message)
        {
            ConfigKey = configKey;
        }

        public ConfigurationException(string message, Exception innerException, string? configKey = null)
            : base(message, innerException)
        {
            ConfigKey = configKey;
        }
    }
}
