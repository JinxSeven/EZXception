using System;

namespace EZXception.Network
{
    /// <summary>
    /// Base exception for all network-related failures.
    /// </summary>
    public class NetworkException : Exception
    {
        public NetworkException(string message)
            : base(message) { }

        public NetworkException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
