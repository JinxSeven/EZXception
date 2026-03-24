using System;

namespace EZXception.Business
{
    /// <summary>
    /// Thrown when a usage quota or limit has been exceeded (e.g. max items, storage, API calls).
    /// </summary>
    public class QuotaExceededException : BusinessRuleException
    {
        public string? QuotaName { get; }
        public long? Limit { get; }
        public long? Current { get; }

        public QuotaExceededException(string quotaName, long? limit = null, long? current = null)
            : base(BuildMessage(quotaName, limit, current))
        {
            QuotaName = quotaName;
            Limit = limit;
            Current = current;
        }

        public QuotaExceededException(string message, Exception innerException)
            : base(message, innerException) { }

        private static string BuildMessage(string quotaName, long? limit, long? current)
        {
            if (limit.HasValue && current.HasValue)
                return $"Quota '{quotaName}' exceeded: {current}/{limit}.";
            if (limit.HasValue)
                return $"Quota '{quotaName}' exceeded. Limit is {limit}.";
            return $"Quota '{quotaName}' has been exceeded.";
        }
    }
}
