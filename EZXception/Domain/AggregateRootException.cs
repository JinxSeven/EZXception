using System;

namespace EZXception.Domain
{
    /// <summary>
    /// Thrown when an operation violates an aggregate root boundary or consistency rule.
    /// </summary>
    public class AggregateRootException : DomainException
    {
        public string? AggregateType { get; }
        public object? AggregateId { get; }

        public AggregateRootException(string aggregateType, object? aggregateId, string message)
            : base(message, aggregateType)
        {
            AggregateType = aggregateType;
            AggregateId = aggregateId;
        }

        public AggregateRootException(string aggregateType, string message, Exception innerException)
            : base(message, innerException, aggregateType)
        {
            AggregateType = aggregateType;
        }
    }
}
