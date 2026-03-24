using System;

namespace EZXception.ExternalService
{
    /// <summary>
    /// Thrown when an external service rate limit (HTTP 429) has been hit.
    /// </summary>
    public class RateLimitException : ExternalServiceException
    {
        public TimeSpan? RetryAfter { get; }

        public RateLimitException(string serviceName, TimeSpan? retryAfter = null)
            : base(serviceName, BuildMessage(serviceName, retryAfter))
        {
            RetryAfter = retryAfter;
        }

        public RateLimitException(string serviceName, string message, Exception innerException)
            : base(serviceName, message, innerException) { }

        private static string BuildMessage(string service, TimeSpan? retryAfter)
        {
            return retryAfter.HasValue
                ? $"Rate limit exceeded for service '{service}'. Retry after {retryAfter.Value.TotalSeconds:F0} seconds."
                : $"Rate limit exceeded for service '{service}'.";
        }
    }
}
