using System;

namespace EZXception.Authorization
{
    /// <summary>
    /// Thrown when an authenticated user does not have permission to perform an action (HTTP 403 equivalent).
    /// </summary>
    public class ForbiddenException : Exception
    {
        public string? RequiredPermission { get; }

        public ForbiddenException()
            : base("You do not have permission to perform this action.") { }

        public ForbiddenException(string message)
            : base(message) { }

        public ForbiddenException(string message, string requiredPermission)
            : base(message)
        {
            RequiredPermission = requiredPermission;
        }

        public ForbiddenException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
