using System;

namespace EZXception.IO
{
    /// <summary>
    /// Thrown when attempting to create a file that already exists.
    /// </summary>
    public class FileAlreadyExistsException : Exception
    {
        public string? FilePath { get; }

        public FileAlreadyExistsException(string filePath)
            : base($"File already exists: '{filePath}'.")
        {
            FilePath = filePath;
        }

        public FileAlreadyExistsException(string filePath, string message)
            : base(message)
        {
            FilePath = filePath;
        }

        public FileAlreadyExistsException(string filePath, string message, Exception innerException)
            : base(message, innerException)
        {
            FilePath = filePath;
        }
    }
}
