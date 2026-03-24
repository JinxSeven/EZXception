using System;

namespace EZXception.Domain
{
    /// <summary>
    /// Thrown when a domain aggregate or entity invariant (an always-true rule) is violated.
    /// </summary>
    public class InvariantViolationException : DomainException
    {
        public string? InvariantName { get; }

        public InvariantViolationException(string invariantName, string message)
            : base(message, invariantName)
        {
            InvariantName = invariantName;
        }

        public InvariantViolationException(string invariantName, string message, Exception innerException)
            : base(message, innerException, invariantName)
        {
            InvariantName = invariantName;
        }
    }
}
