using System;
using System.Data.Common;

namespace GungeonAlly.DatabaseCore
{
    public static class DbCommandExtensions
    {
        public static void AddParameters(this DbCommand dbCommand, params DbParameter[] commandParameters)
        {
            dbCommand.Parameters.AddRange(commandParameters);
        }

        public static DbParameter CreateParameter(this DbCommand dbCommand, string parameterName, object parameterValue)
        {
            var p = dbCommand.CreateParameter();
            p.ParameterName = parameterName;
            p.Value = parameterValue;
            return p;
        }

        /// <summary>Create a Database Command parameter which replaces a NULL
        /// object with the Database "DBNull" value, or passes the value if
        /// it is non-null.</summary>
        /// <param name="dbCommand">Database Command to extend</param>
        /// <param name="parameterName">Name of the parameter to create</param>
        /// <param name="parameterValue">Nullable Parameter value</param>
        /// <returns>Database Parameter with a DBNull value if the object
        /// passed has a "null" value in .Net.</returns>
        public static DbParameter CreateNullableParameter(
            this DbCommand dbCommand, string parameterName, object parameterValue)
        {
            var p = dbCommand.CreateParameter();
            p.ParameterName = parameterName;
            if (parameterValue == null)
            {
                p.Value = DBNull.Value;
            }
            else
            {
                p.Value = parameterValue;
            }
            return p;
        }
    }
}
