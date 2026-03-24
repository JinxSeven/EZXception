using System;

namespace EZXception.Data
{
    /// <summary>
    /// Thrown when attempting to create an entity that already exists (HTTP 409 equivalent).
    /// </summary>
    public class DuplicateEntityException : Exception
    {
        public string? EntityType { get; }
        public object? ConflictingValue { get; }

        public DuplicateEntityException(string entityType, object? conflictingValue = null)
            : base(conflictingValue != null
                ? $"A {entityType} with value '{conflictingValue}' already exists."
                : $"A duplicate {entityType} already exists.")
        {
            EntityType = entityType;
            ConflictingValue = conflictingValue;
        }

        public DuplicateEntityException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
