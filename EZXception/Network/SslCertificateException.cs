using System;

namespace EZXception.Network
{
    /// <summary>
    /// Thrown when an SSL/TLS certificate is invalid, expired, untrusted, or cannot be verified.
    /// </summary>
    public class SslCertificateException : NetworkException
    {
        public string? Host { get; }

        public SslCertificateException(string host, string reason)
            : base($"SSL certificate error for '{host}': {reason}.")
        {
            Host = host;
        }

        public SslCertificateException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
