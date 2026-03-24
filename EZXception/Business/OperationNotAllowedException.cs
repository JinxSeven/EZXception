using System;

namespace EZXception.Business
{
    /// <summary>
    /// Thrown when an operation is logically not permitted given the current state of the system or entity.
    /// </summary>
    public class OperationNotAllowedException : BusinessRuleException
    {
        public string? OperationName { get; }

        public OperationNotAllowedException(string operationName, string reason)
            : base($"Operation '{operationName}' is not allowed: {reason}")
        {
            OperationName = operationName;
        }

        public OperationNotAllowedException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
