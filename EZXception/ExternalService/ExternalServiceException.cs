using System;

namespace EZXception.ExternalService
{
    /// <summary>
    /// Base exception for failures communicating with or receiving errors from an external service.
    /// </summary>
    public class ExternalServiceException : Exception
    {
        public string? ServiceName { get; }

        public ExternalServiceException(string serviceName, string message)
            : base(message)
        {
            ServiceName = serviceName;
        }

        public ExternalServiceException(string serviceName, string message, Exception innerException)
            : base(message, innerException)
        {
            ServiceName = serviceName;
        }
    }
}
