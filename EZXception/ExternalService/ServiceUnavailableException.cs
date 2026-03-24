using System;

namespace EZXception.ExternalService
{
    /// <summary>
    /// Thrown when an external service is unreachable or temporarily unavailable (HTTP 503 equivalent).
    /// </summary>
    public class ServiceUnavailableException : ExternalServiceException
    {
        public ServiceUnavailableException(string serviceName)
            : base(serviceName, $"Service '{serviceName}' is currently unavailable. Please try again later.") { }

        public ServiceUnavailableException(string serviceName, string message)
            : base(serviceName, message) { }

        public ServiceUnavailableException(string serviceName, string message, Exception innerException)
            : base(serviceName, message, innerException) { }
    }
}
