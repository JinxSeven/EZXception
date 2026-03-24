using System;

namespace EZXception.Concurrency
{
    /// <summary>
    /// Thrown when a deadlock condition is detected (two or more operations blocking each other indefinitely).
    /// </summary>
    public class DeadlockException : Exception
    {
        public DeadlockException()
            : base("A deadlock was detected. The operation cannot proceed.") { }

        public DeadlockException(string message)
            : base(message) { }

        public DeadlockException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
