using System;

namespace EZXception.Business
{
    /// <summary>
    /// Thrown when an entity attempts to transition to a state that is not allowed from its current state.
    /// </summary>
    public class InvalidStateTransitionException : BusinessRuleException
    {
        public string? EntityType { get; }
        public string? FromState { get; }
        public string? ToState { get; }

        public InvalidStateTransitionException(string? entityType, string fromState, string toState)
            : base(BuildMessage(entityType, fromState, toState))
        {
            EntityType = entityType;
            FromState = fromState;
            ToState = toState;
        }

        public InvalidStateTransitionException(string message, Exception innerException)
            : base(message, innerException) { }

        private static string BuildMessage(string? entityType, string from, string to)
        {
            var subject = entityType ?? "Entity";
            return $"{subject} cannot transition from '{from}' to '{to}'.";
        }
    }
}
