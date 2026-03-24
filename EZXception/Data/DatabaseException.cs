using System;

namespace EZXception.Data
{
    /// <summary>
    /// Thrown when a general database operation fails (connection, query, transaction, etc.).
    /// </summary>
    public class DatabaseException : Exception
    {
        public string? Operation { get; }

        public DatabaseException(string message, string? operation = null)
            : base(message)
        {
            Operation = operation;
        }

        public DatabaseException(string message, Exception innerException, string? operation = null)
            : base(message, innerException)
        {
            Operation = operation;
        }
    }
}
