using System;

namespace EZXception.IO
{
    /// <summary>
    /// Thrown when a file or directory cannot be read, written, or accessed due to permission or lock issues.
    /// </summary>
    public class FileAccessException : Exception
    {
        public string? FilePath { get; }
        public string? AccessType { get; }

        public FileAccessException(string filePath, string? accessType = null)
            : base(accessType != null
                ? $"Cannot {accessType} file '{filePath}'. Access denied or file is locked."
                : $"Access to file '{filePath}' was denied or the file is locked.")
        {
            FilePath = filePath;
            AccessType = accessType;
        }

        public FileAccessException(string filePath, string message, Exception innerException)
            : base(message, innerException)
        {
            FilePath = filePath;
        }
    }
}
