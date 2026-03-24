using System;

namespace EZXception.Network
{
    /// <summary>
    /// Thrown when a hostname cannot be resolved via DNS.
    /// </summary>
    public class DnsResolutionException : NetworkException
    {
        public string? Hostname { get; }

        public DnsResolutionException(string hostname)
            : base($"DNS resolution failed for host '{hostname}'.")
        {
            Hostname = hostname;
        }

        public DnsResolutionException(string hostname, string message, Exception innerException)
            : base(message, innerException)
        {
            Hostname = hostname;
        }
    }
}
