using System;

namespace EZXception.IO
{
    /// <summary>
    /// Thrown when an expected directory does not exist.
    /// </summary>
    public class EZDirectoryNotFoundException : Exception
    {
        public string? DirectoryPath { get; }

        public EZDirectoryNotFoundException(string directoryPath)
            : base($"Directory not found: '{directoryPath}'.")
        {
            DirectoryPath = directoryPath;
        }

        public EZDirectoryNotFoundException(string directoryPath, string message)
            : base(message)
        {
            DirectoryPath = directoryPath;
        }

        public EZDirectoryNotFoundException(string directoryPath, string message, Exception innerException)
            : base(message, innerException)
        {
            DirectoryPath = directoryPath;
        }
    }
}
