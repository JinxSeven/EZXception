using System;

namespace EZXception.IO
{
    /// <summary>
    /// Thrown when an expected file does not exist at the given path.
    /// </summary>
    public class EZFileNotFoundException : Exception
    {
        public string? FilePath { get; }

        public EZFileNotFoundException(string filePath)
            : base($"File not found: '{filePath}'.")
        {
            FilePath = filePath;
        }

        public EZFileNotFoundException(string filePath, string message)
            : base(message)
        {
            FilePath = filePath;
        }

        public EZFileNotFoundException(string filePath, string message, Exception innerException)
            : base(message, innerException)
        {
            FilePath = filePath;
        }
    }
}
