using System;
using System.Collections.Generic;

namespace EZXception.Validation
{
    /// <summary>
    /// Thrown when one or more validation rules fail on an object or input.
    /// </summary>
    public class ValidationException : Exception
    {
        public IReadOnlyList<string> Errors { get; }

        public ValidationException(string message)
            : base(message)
        {
            Errors = new List<string> { message };
        }

        public ValidationException(string message, IEnumerable<string> errors)
            : base(message)
        {
            Errors = new List<string>(errors);
        }

        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
            Errors = new List<string> { message };
        }
    }
}
