using System;

namespace EZXception.Authorization
{
    /// <summary>
    /// Thrown when a user is not authenticated (HTTP 401 equivalent).
    /// </summary>
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException()
            : base("Authentication is required to access this resource.") { }

        public UnauthorizedException(string message)
            : base(message) { }

        public UnauthorizedException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
