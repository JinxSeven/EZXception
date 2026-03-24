using System;

namespace EZXception.Network
{
    /// <summary>
    /// Thrown when a network request fails due to a proxy error or misconfiguration.
    /// </summary>
    public class ProxyException : NetworkException
    {
        public string? ProxyAddress { get; }

        public ProxyException(string message, string? proxyAddress = null)
            : base(message)
        {
            ProxyAddress = proxyAddress;
        }

        public ProxyException(string message, Exception innerException, string? proxyAddress = null)
            : base(message, innerException)
        {
            ProxyAddress = proxyAddress;
        }
    }
}
