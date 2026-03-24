using System;

namespace EZXception.Authorization
{
    /// <summary>
    /// Thrown when a user does not have the required role to access a resource or perform an action.
    /// </summary>
    public class InsufficientRoleException : ForbiddenException
    {
        public string? RequiredRole { get; }
        public string? ActualRole { get; }

        public InsufficientRoleException(string requiredRole, string? actualRole = null)
            : base(BuildMessage(requiredRole, actualRole), requiredRole)
        {
            RequiredRole = requiredRole;
            ActualRole = actualRole;
        }

        public InsufficientRoleException(string message, Exception innerException)
            : base(message, innerException) { }

        private static string BuildMessage(string required, string? actual)
        {
            return actual != null
                ? $"Role '{required}' is required, but user has role '{actual}'."
                : $"Role '{required}' is required to perform this action.";
        }
    }
}
