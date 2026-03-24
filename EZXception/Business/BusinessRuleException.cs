using System;

namespace EZXception.Business
{
    /// <summary>
    /// Thrown when a domain/business rule is violated.
    /// </summary>
    public class BusinessRuleException : Exception
    {
        public string? RuleName { get; }

        public BusinessRuleException(string message, string? ruleName = null)
            : base(message)
        {
            RuleName = ruleName;
        }

        public BusinessRuleException(string message, Exception innerException, string? ruleName = null)
            : base(message, innerException)
        {
            RuleName = ruleName;
        }
    }
}
