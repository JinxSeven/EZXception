using System;

namespace EZXception.Authorization
{
    /// <summary>
    /// Thrown when provided credentials (username/password, API key, etc.) are invalid.
    /// </summary>
    public class InvalidCredentialsException : UnauthorizedException
    {
        public InvalidCredentialsException()
            : base("The provided credentials are invalid.") { }

        public InvalidCredentialsException(string message)
            : base(message) { }

        public InvalidCredentialsException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
