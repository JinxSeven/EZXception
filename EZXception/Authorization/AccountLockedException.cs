using System;

namespace EZXception.Authorization
{
    /// <summary>
    /// Thrown when a user account is locked due to too many failed attempts or administrative action.
    /// </summary>
    public class AccountLockedException : ForbiddenException
    {
        public string? UserId { get; }
        public DateTimeOffset? LockedUntil { get; }

        public AccountLockedException(string? userId = null, DateTimeOffset? lockedUntil = null)
            : base(BuildMessage(userId, lockedUntil))
        {
            UserId = userId;
            LockedUntil = lockedUntil;
        }

        public AccountLockedException(string message, Exception innerException)
            : base(message, innerException) { }

        private static string BuildMessage(string? userId, DateTimeOffset? lockedUntil)
        {
            var who = userId != null ? $"Account '{userId}'" : "This account";
            return lockedUntil.HasValue
                ? $"{who} is locked until {lockedUntil:u}."
                : $"{who} is locked. Please contact support.";
        }
    }
}
