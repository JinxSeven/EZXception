using System;

namespace EZXception.Serialization
{
    /// <summary>
    /// Thrown when serializing an object to a target format (JSON, XML, binary, etc.) fails.
    /// </summary>
    public class EZSerializationException : Exception
    {
        public string? TargetFormat { get; }
        public Type? TargetType { get; }

        public EZSerializationException(string message, string? targetFormat = null, Type? targetType = null)
            : base(message)
        {
            TargetFormat = targetFormat;
            TargetType = targetType;
        }

        public EZSerializationException(string message, Exception innerException, string? targetFormat = null, Type? targetType = null)
            : base(message, innerException)
        {
            TargetFormat = targetFormat;
            TargetType = targetType;
        }
    }
}
