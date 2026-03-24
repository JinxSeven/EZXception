using System;

namespace EZXception.Authorization
{
    /// <summary>
    /// Thrown when an authentication token (JWT, API key, session, etc.) has expired.
    /// </summary>
    public class TokenExpiredException : UnauthorizedException
    {
        public DateTimeOffset? ExpiresAt { get; }

        public TokenExpiredException()
            : base("The authentication token has expired. Please re-authenticate.") { }

        public TokenExpiredException(DateTimeOffset expiresAt)
            : base($"The authentication token expired at {expiresAt:u}.")
        {
            ExpiresAt = expiresAt;
        }

        public TokenExpiredException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
