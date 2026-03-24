using System;

namespace EZXception.Validation
{
    /// <summary>
    /// Thrown when a string value exceeds or falls below the allowed length.
    /// </summary>
    public class StringLengthException : ValidationException
    {
        public string FieldName { get; }
        public int? MinLength { get; }
        public int? MaxLength { get; }
        public int ActualLength { get; }

        public StringLengthException(string fieldName, int actualLength, int? minLength = null, int? maxLength = null)
            : base(BuildMessage(fieldName, actualLength, minLength, maxLength))
        {
            FieldName = fieldName;
            ActualLength = actualLength;
            MinLength = minLength;
            MaxLength = maxLength;
        }

        public StringLengthException(string fieldName, string message, Exception innerException)
            : base(message, innerException)
        {
            FieldName = fieldName;
        }

        private static string BuildMessage(string fieldName, int actual, int? min, int? max)
        {
            if (min.HasValue && max.HasValue)
                return $"'{fieldName}' must be between {min} and {max} characters. Actual length: {actual}.";
            if (max.HasValue)
                return $"'{fieldName}' must not exceed {max} characters. Actual length: {actual}.";
            if (min.HasValue)
                return $"'{fieldName}' must be at least {min} characters. Actual length: {actual}.";
            return $"'{fieldName}' has an invalid length of {actual}.";
        }
    }
}
