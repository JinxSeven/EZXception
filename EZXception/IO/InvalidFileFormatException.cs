using System;

namespace EZXception.IO
{
    /// <summary>
    /// Thrown when a file's content does not match the expected format or structure.
    /// </summary>
    public class InvalidFileFormatException : Exception
    {
        public string? FilePath { get; }
        public string? ExpectedFormat { get; }

        public InvalidFileFormatException(string filePath, string? expectedFormat = null)
            : base(expectedFormat != null
                ? $"File '{filePath}' does not match the expected format '{expectedFormat}'."
                : $"File '{filePath}' has an invalid or unrecognized format.")
        {
            FilePath = filePath;
            ExpectedFormat = expectedFormat;
        }

        public InvalidFileFormatException(string filePath, string message, Exception innerException)
            : base(message, innerException)
        {
            FilePath = filePath;
        }
    }
}
