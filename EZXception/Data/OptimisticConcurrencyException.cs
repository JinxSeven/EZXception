using System;

namespace EZXception.Data
{
    /// <summary>
    /// Thrown when an optimistic concurrency conflict is detected — the record was modified
    /// by another process between the read and the write.
    /// </summary>
    public class OptimisticConcurrencyException : Exception
    {
        public string? EntityType { get; }
        public object? EntityId { get; }

        public OptimisticConcurrencyException(string? entityType = null, object? entityId = null)
            : base(BuildMessage(entityType, entityId))
        {
            EntityType = entityType;
            EntityId = entityId;
        }

        public OptimisticConcurrencyException(string message, Exception innerException)
            : base(message, innerException) { }

        private static string BuildMessage(string? entityType, object? entityId)
        {
            if (entityType != null && entityId != null)
                return $"{entityType} with id '{entityId}' was modified by another process. Please reload and retry.";
            if (entityType != null)
                return $"{entityType} was modified by another process. Please reload and retry.";
            return "A concurrency conflict occurred. The record was modified by another process. Please reload and retry.";
        }
    }
}
