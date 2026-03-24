using System;

namespace EZXception.Validation
{
    /// <summary>
    /// Thrown when a required field is null, empty, or missing.
    /// </summary>
    public class RequiredFieldException : ValidationException
    {
        public string FieldName { get; }

        public RequiredFieldException(string fieldName)
            : base($"The field '{fieldName}' is required and cannot be null or empty.")
        {
            FieldName = fieldName;
        }

        public RequiredFieldException(string fieldName, string message)
            : base(message)
        {
            FieldName = fieldName;
        }

        public RequiredFieldException(string fieldName, string message, Exception innerException)
            : base(message, innerException)
        {
            FieldName = fieldName;
        }
    }
}
