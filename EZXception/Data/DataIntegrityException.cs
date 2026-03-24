using System;

namespace EZXception.Data
{
    /// <summary>
    /// Thrown when a database constraint or data integrity rule is violated (e.g. foreign key, unique constraint).
    /// </summary>
    public class DataIntegrityException : Exception
    {
        public string? ConstraintName { get; }

        public DataIntegrityException(string message, string? constraintName = null)
            : base(message)
        {
            ConstraintName = constraintName;
        }

        public DataIntegrityException(string message, Exception innerException, string? constraintName = null)
            : base(message, innerException)
        {
            ConstraintName = constraintName;
        }
    }
}
