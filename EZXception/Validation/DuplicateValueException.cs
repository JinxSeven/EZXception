using System;

namespace EZXception.Validation
{
    /// <summary>
    /// Thrown when a value already exists where uniqueness is required.
    /// </summary>
    public class DuplicateValueException : ValidationException
    {
        public string FieldName { get; }
        public object? Value { get; }

        public DuplicateValueException(string fieldName, object? value = null)
            : base(value != null
                ? $"A duplicate value '{value}' already exists for '{fieldName}'."
                : $"A duplicate value already exists for '{fieldName}'.")
        {
            FieldName = fieldName;
            Value = value;
        }

        public DuplicateValueException(string fieldName, string message, Exception innerException)
            : base(message, innerException)
        {
            FieldName = fieldName;
        }
    }
}
