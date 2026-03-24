using System;

namespace EZXception.Data
{
    /// <summary>
    /// Thrown when a requested entity does not exist in the data store (HTTP 404 equivalent).
    /// </summary>
    public class EntityNotFoundException : Exception
    {
        public string? EntityType { get; }
        public object? EntityId { get; }

        public EntityNotFoundException(string entityType, object? entityId = null)
            : base(entityId != null
                ? $"{entityType} with id '{entityId}' was not found."
                : $"{entityType} was not found.")
        {
            EntityType = entityType;
            EntityId = entityId;
        }

        public EntityNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
