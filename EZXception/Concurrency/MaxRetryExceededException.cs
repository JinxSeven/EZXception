using System;

namespace EZXception.Concurrency
{
    /// <summary>
    /// Thrown when an operation has been retried the maximum number of times without success.
    /// </summary>
    public class MaxRetryExceededException : Exception
    {
        public int Attempts { get; }
        public string? OperationName { get; }

        public MaxRetryExceededException(int attempts, string? operationName = null)
            : base(operationName != null
                ? $"Operation '{operationName}' failed after {attempts} attempts."
                : $"Operation failed after {attempts} retry attempts.")
        {
            Attempts = attempts;
            OperationName = operationName;
        }

        public MaxRetryExceededException(int attempts, string operationName, Exception innerException)
            : base($"Operation '{operationName}' failed after {attempts} attempts.", innerException)
        {
            Attempts = attempts;
            OperationName = operationName;
        }
    }
}
