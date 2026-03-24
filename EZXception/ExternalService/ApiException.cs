using System;

namespace EZXception.ExternalService
{
    /// <summary>
    /// Thrown when an external HTTP API returns an error response.
    /// </summary>
    public class ApiException : ExternalServiceException
    {
        public int? StatusCode { get; }
        public string? ResponseBody { get; }
        public string? RequestUrl { get; }

        public ApiException(string serviceName, int statusCode, string? responseBody = null, string? requestUrl = null)
            : base(serviceName, BuildMessage(serviceName, statusCode, requestUrl))
        {
            StatusCode = statusCode;
            ResponseBody = responseBody;
            RequestUrl = requestUrl;
        }

        public ApiException(string serviceName, string message, Exception innerException)
            : base(serviceName, message, innerException) { }

        private static string BuildMessage(string service, int statusCode, string? url)
        {
            return url != null
                ? $"API call to '{service}' at '{url}' failed with status {statusCode}."
                : $"API call to '{service}' failed with status {statusCode}.";
        }
    }
}
