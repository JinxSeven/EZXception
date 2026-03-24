using System;

namespace EZXception.ExternalService
{
    /// <summary>
    /// Thrown when an operation or external call exceeds its allowed time limit.
    /// </summary>
    public class OperationTimeoutException : ExternalServiceException
    {
        public TimeSpan? Timeout { get; }
        public string? OperationName { get; }

        public OperationTimeoutException(string serviceName, string operationName, TimeSpan? timeout = null)
            : base(serviceName, BuildMessage(serviceName, operationName, timeout))
        {
            OperationName = operationName;
            Timeout = timeout;
        }

        public OperationTimeoutException(string serviceName, string message, Exception innerException)
            : base(serviceName, message, innerException) { }

        private static string BuildMessage(string service, string op, TimeSpan? timeout)
        {
            return timeout.HasValue
                ? $"Operation '{op}' on service '{service}' timed out after {timeout.Value.TotalSeconds:F1}s."
                : $"Operation '{op}' on service '{service}' timed out.";
        }
    }
}
