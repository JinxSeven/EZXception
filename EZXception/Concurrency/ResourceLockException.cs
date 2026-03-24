using System;

namespace EZXception.Concurrency
{
    /// <summary>
    /// Thrown when a resource cannot be acquired because it is locked by another process or thread.
    /// </summary>
    public class ResourceLockException : Exception
    {
        public string? ResourceName { get; }

        public ResourceLockException(string? resourceName = null)
            : base(resourceName != null
                ? $"Resource '{resourceName}' is currently locked and cannot be acquired."
                : "A required resource is currently locked and cannot be acquired.")
        {
            ResourceName = resourceName;
        }

        public ResourceLockException(string resourceName, string message, Exception innerException)
            : base(message, innerException)
        {
            ResourceName = resourceName;
        }
    }
}
