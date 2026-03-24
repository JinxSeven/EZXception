using System;

namespace EZXception.Data
{
    /// <summary>
    /// Thrown when a data query fails (malformed query, invalid filter, unsupported operation, etc.).
    /// </summary>
    public class QueryException : DatabaseException
    {
        public string? Query { get; }

        public QueryException(string message, string? query = null)
            : base(message, "Query")
        {
            Query = query;
        }

        public QueryException(string message, Exception innerException, string? query = null)
            : base(message, innerException, "Query")
        {
            Query = query;
        }
    }
}
