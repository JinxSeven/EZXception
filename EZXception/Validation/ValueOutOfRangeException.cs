using System;

namespace EZXception.Validation
{
    /// <summary>
    /// Thrown when a value falls outside an allowed range.
    /// </summary>
    public class ValueOutOfRangeException : ValidationException
    {
        public string FieldName { get; }
        public object? MinValue { get; }
        public object? MaxValue { get; }
        public object? ActualValue { get; }

        public ValueOutOfRangeException(string fieldName, object? minValue, object? maxValue, object? actualValue = null)
            : base(BuildMessage(fieldName, minValue, maxValue, actualValue))
        {
            FieldName = fieldName;
            MinValue = minValue;
            MaxValue = maxValue;
            ActualValue = actualValue;
        }

        public ValueOutOfRangeException(string fieldName, string message)
            : base(message)
        {
            FieldName = fieldName;
        }

        public ValueOutOfRangeException(string fieldName, string message, Exception innerException)
            : base(message, innerException)
        {
            FieldName = fieldName;
        }

        private static string BuildMessage(string fieldName, object? min, object? max, object? actual)
        {
            var range = $"[{min ?? "∞"}, {max ?? "∞"}]";
            return actual != null
                ? $"Value '{actual}' for '{fieldName}' is out of the allowed range {range}."
                : $"Value for '{fieldName}' is out of the allowed range {range}.";
        }
    }
}
