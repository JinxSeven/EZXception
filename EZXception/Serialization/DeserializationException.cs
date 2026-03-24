using System;

namespace EZXception.Serialization
{
    /// <summary>
    /// Thrown when deserializing data from a format (JSON, XML, binary, etc.) into an object fails.
    /// </summary>
    public class DeserializationException : Exception
    {
        public string? SourceFormat { get; }
        public Type? TargetType { get; }
        public string? RawInput { get; }

        public DeserializationException(string message, string? sourceFormat = null, Type? targetType = null, string? rawInput = null)
            : base(message)
        {
            SourceFormat = sourceFormat;
            TargetType = targetType;
            RawInput = rawInput;
        }

        public DeserializationException(string message, Exception innerException, string? sourceFormat = null, Type? targetType = null)
            : base(message, innerException)
        {
            SourceFormat = sourceFormat;
            TargetType = targetType;
        }
    }
}
