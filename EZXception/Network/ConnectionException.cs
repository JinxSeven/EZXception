using System;

namespace EZXception.Network
{
    /// <summary>
    /// Thrown when a network connection cannot be established or is unexpectedly dropped.
    /// </summary>
    public class ConnectionException : NetworkException
    {
        public string? Host { get; }
        public int? Port { get; }

        public ConnectionException(string? host = null, int? port = null)
            : base(BuildMessage(host, port))
        {
            Host = host;
            Port = port;
        }

        public ConnectionException(string message, Exception innerException)
            : base(message, innerException) { }

        private static string BuildMessage(string? host, int? port)
        {
            if (host != null && port.HasValue)
                return $"Could not connect to '{host}:{port}'.";
            if (host != null)
                return $"Could not connect to '{host}'.";
            return "A network connection could not be established.";
        }
    }
}
