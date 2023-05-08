using System;

namespace GungeonAlly.DatabaseCore
{
    /// <summary>Parsing exception occurred trying to parse a column field 
    /// using the IDataRecordExtensions ParseByColumnMap method.</summary>
    public class ColumnMapParseException : Exception
    {
        /// <summary>Create a base exception showing the column name and 
        /// value being parsed.  The Exception Message will describe the
        /// column Name and the first 30 characters of the value as a 
        /// string.</summary>
        /// <param name="colName">Name of the Column being parsed</param>
        /// <param name="colValue">Value for the column being parsed</param>
        /// <param name="ex">Exception that occurred during parsing</param>
        public ColumnMapParseException(string colName, object colValue, Exception ex)
            : base(string.Format("Error Parsing [{0}{1}] for column [{2}] - {3}",
                (colValue?.ToString() ?? "").Substring(0, Math.Min((colValue?.ToString() ?? "").Length, 30)),
                (colValue?.ToString() ?? "").Length > 30 ? "..." : "", colName, ex.Message), ex)
        {
            ColumnName = colName;
            ColumnValue = colValue;
        }

        /// <summary>Name of the column being parsed</summary>
        public string ColumnName { get; private set; }

        /// <summary>Value from the column being parsed</summary>
        public object ColumnValue { get; private set; }
    }
}