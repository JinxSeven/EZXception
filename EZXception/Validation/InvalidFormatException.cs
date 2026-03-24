using System;

namespace EZXception.Validation
{
    /// <summary>
    /// Thrown when a value does not match an expected format (e.g. email, phone, date).
    /// </summary>
    public class InvalidFormatException : ValidationException
    {
        public string FieldName { get; }
        public string? ExpectedFormat { get; }
        public object? ActualValue { get; }

        public InvalidFormatException(string fieldName, string? expectedFormat = null, object? actualValue = null)
            : base(BuildMessage(fieldName, expectedFormat, actualValue))
        {
            FieldName = fieldName;
            ExpectedFormat = expectedFormat;
            ActualValue = actualValue;
        }

        public InvalidFormatException(string fieldName, string message, Exception innerException)
            : base(message, innerException)
        {
            FieldName = fieldName;
        }

        private static string BuildMessage(string fieldName, string? expectedFormat, object? actualValue)
        {
            if (expectedFormat != null && actualValue != null)
                return $"'{fieldName}' has an invalid format. Expected: '{expectedFormat}', Got: '{actualValue}'.";
            if (expectedFormat != null)
                return $"'{fieldName}' has an invalid format. Expected: '{expectedFormat}'.";
            return $"'{fieldName}' has an invalid format.";
        }
    }
}
