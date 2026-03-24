using System;

namespace EZXception.IO
{
    /// <summary>
    /// Thrown when a file exceeds the maximum allowed size.
    /// </summary>
    public class FileSizeLimitException : Exception
    {
        public string? FilePath { get; }
        public long? MaxSizeBytes { get; }
        public long? ActualSizeBytes { get; }

        public FileSizeLimitException(string filePath, long maxSizeBytes, long actualSizeBytes)
            : base($"File '{filePath}' exceeds the maximum allowed size of {FormatBytes(maxSizeBytes)}. Actual size: {FormatBytes(actualSizeBytes)}.")
        {
            FilePath = filePath;
            MaxSizeBytes = maxSizeBytes;
            ActualSizeBytes = actualSizeBytes;
        }

        public FileSizeLimitException(string filePath, string message, Exception innerException)
            : base(message, innerException)
        {
            FilePath = filePath;
        }

        private static string FormatBytes(long bytes)
        {
            if (bytes >= 1_073_741_824) return $"{bytes / 1_073_741_824.0:F2} GB";
            if (bytes >= 1_048_576) return $"{bytes / 1_048_576.0:F2} MB";
            if (bytes >= 1024) return $"{bytes / 1024.0:F2} KB";
            return $"{bytes} B";
        }
    }
}
