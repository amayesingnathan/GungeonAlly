using System;
using System.Data;
using System.Data.Common;

namespace GungeonAlly.DatabaseCore
{
    public static class DbConnectionExtensions
    {
        public static DbCommand CreateCommand(this DbConnection dbConnection, string commandText, CommandType commandType = CommandType.Text, int? commandTimeout = null)
        {
            var dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = commandText;
            dbCommand.CommandType = commandType;
            if (commandTimeout.HasValue)
            {
                dbCommand.CommandTimeout = commandTimeout.Value;
            }
            return dbCommand;
        }

        public static DbDataReader ExecuteReader(this DbConnection dbConnection, string commandText, CommandType commandType = CommandType.Text, int? commandTimeout = null, Func<DbCommand, DbParameter[]> parameterBinding= null)
        {
            var cmd = dbConnection.CreateCommand(commandText, commandType, commandTimeout);
            var dbParameters = parameterBinding?.Invoke(cmd);
            if (dbParameters != null)
            {
                cmd.AddParameters(dbParameters);
            }
            return cmd.ExecuteReader();
        }

        /// <summary>Execute the Database command with no data being read from the response.</summary>
        /// <param name="dbConnection">Database connection to connect to</param>
        /// <param name="commandText">Command Text</param>
        /// <param name="commandType">Command Type</param>
        /// <param name="commandTimeout">Commamd Timeout</param>
        /// <param name="parameterBinding">Request Parameters</param>
        /// <returns>The number of rows affected.</returns>
        public static int ExecuteNonQuery(this DbConnection dbConnection,
            string commandText, CommandType commandType = CommandType.Text,
            int? commandTimeout = null, Func<DbCommand, DbParameter[]> parameterBinding = null)
        {
            var cmd = dbConnection.CreateCommand(commandText, commandType, commandTimeout);
            var dbParameters = parameterBinding?.Invoke(cmd);
            if (dbParameters != null)
            {
                cmd.AddParameters(dbParameters);
            }
            return cmd.ExecuteNonQuery();
        }
    }
}
