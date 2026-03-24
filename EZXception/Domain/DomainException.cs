using System;

namespace EZXception.Domain
{
    /// <summary>
    /// Base exception for all domain model violations. Use this as a base for rich domain errors.
    /// </summary>
    public class DomainException : Exception
    {
        public string? DomainName { get; }

        public DomainException(string message, string? domainName = null)
            : base(message)
        {
            DomainName = domainName;
        }

        public DomainException(string message, Exception innerException, string? domainName = null)
            : base(message, innerException)
        {
            DomainName = domainName;
        }
    }
}
