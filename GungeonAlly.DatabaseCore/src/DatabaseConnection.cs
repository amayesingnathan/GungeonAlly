using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;

using Microsoft.Extensions.Configuration;

using GungeonAlly.DatabaseCore.ColumnAttribute;
using System.Data.SqlClient;

namespace GungeonAlly.DatabaseCore
{
    public class DatabaseConnection
    {
        #region private members
        DbProviderFactory? _DbProvider;
        DbCommandBuilder? _DbCommandBuilder;
        #endregion

        #region public properties

        /// <summary>
        /// Database Connection String
        /// </summary>
        public string? ConnectionString { get; private set; }

        /// <summary>
        /// Database Provider Name
        /// </summary>
        public const string ProviderName = "System.Data.SqlClient";

        /// <summary>
        /// Is the database connection correctly initialised
        /// </summary>
        public bool IsValid { get; private set; } = false;

        /// <summary>
        /// Default command execution timeout (seconds)
        /// </summary>
        public int DefaultCommandTimeout { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionStringName">Connection string name.</param>
        public DatabaseConnection(string connectionString)
        {
            // 
            // Common connection setup
            //
            InitialiseConnection(connectionString);
        }

        /// <summary>
        /// Setup database provider and connectio settings
        /// </summary>
        /// <param name="connectionString">Database connection string</param>
        /// <param name="providerName">Invariant name of a provider</param>
        private void InitialiseConnection(string connectionString)
        {
            //
            // Validate passed parameters and store
            // 
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Missing database connection string.", nameof(connectionString));
            }
            ConnectionString = connectionString;
            //
            // Successfully configured so mark as valid.
            //
            IsValid = true;
        }
        #endregion

        #region Parameter handling

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <param name="dbType"></param>
        /// <param name="parameterDirection"></param>
        /// <returns></returns>
        public DbParameter CreateParameter(
            string? parameterName = null,
            object? value = null,
            DbType? dbType = null,
            ParameterDirection parameterDirection = ParameterDirection.Input
            )
        {
            var p = _DbProvider.CreateParameter();
            p.ParameterName = parameterName;
            p.Value = value;
            p.Direction = parameterDirection;
            if (dbType.HasValue)
            {
                p.DbType = dbType.Value;
            }
            return p;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public DbParameter CreateParameter(object parameter)
        {
            if (parameter is DbParameter)
            {
                return parameter as DbParameter;
            }
            if (parameter is KeyValuePair<string, object>)
            {
                var p = (parameter as KeyValuePair<string, object>?);
                return CreateParameter(p?.Key, p?.Value);
            }
            return CreateParameter(value: parameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<DbParameter> CreateParameters(params object[] parameters)
        {
            return parameters.Select(arg => CreateParameter(arg));
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="openConnection"></param>
        /// <returns></returns>
        public DbConnection GetDbConnection(bool openConnection = true)
        {
            var dbConnection = new SqlConnection(ConnectionString);
            if (openConnection)
            {
                dbConnection.Open();
            }
            return dbConnection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public DbCommand CreateCommand(DbConnection dbConnection, string commandText, CommandType commandType = CommandType.Text, int? commandTimeout = null, params object[] commandParameters)
        {
            var dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = commandText;
            dbCommand.CommandType = commandType;
            dbCommand.CommandTimeout = commandTimeout ?? DefaultCommandTimeout;
            dbCommand.Parameters.AddRange(CreateParameters(commandParameters));
            return dbCommand;
        }

        #region Command Execution

        #region ExecuteNonQuery

        /// <summary>
        /// Execute Non Query SQL on a new database connection
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string commandText, CommandType commandType = CommandType.Text, int? commandTimeout = null, params object[] commandParameters)
        {
            using (var dbc = GetDbConnection())
            {
                return ExecuteNonQuery(dbc, commandText, commandType, commandTimeout, commandParameters);
            }
        }

        /// <summary>
        /// Execute Non Query SQL on an existing database connection
        /// </summary>
        /// <param name="dbConnection">Database Connection</param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(DbConnection dbConnection, string commandText, CommandType commandType = CommandType.Text, int? commandTimeout = null, params object[] commandParameters)
        {
            var cmd = CreateCommand(dbConnection, commandText, commandType, commandTimeout, commandParameters);
            return cmd.ExecuteNonQuery();
        }

        #endregion

        #region ExecuteScalar

        /// <summary>
        /// Execute SQL returning a single value using an existing database connection and convert the resulting values type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public T ExecuteScalar<T>(DbConnection dbConnection, string commandText, CommandType commandType = CommandType.Text, int? commandTimeout = null, params object[] commandParameters)
        {
            var obj = ExecuteScalar(dbConnection, commandText, commandType, commandTimeout, commandParameters);
            return (T)Convert.ChangeType(obj, typeof(T));
        }

        /// <summary>
        /// Execute SQL returning a single value using an existing database connection
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public object ExecuteScalar(DbConnection dbConnection, string commandText, CommandType commandType = CommandType.Text, int? commandTimeout = null, params object[] commandParameters)
        {
            var cmd = CreateCommand(dbConnection, commandText, commandType, commandTimeout, commandParameters);
            return cmd.ExecuteScalar();
        }

        /// <summary>
        /// Execute SQL returning a single value using a new database connection and convert the resulting values type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public T ExecuteScalar<T>(string commandText, CommandType commandType = CommandType.Text, int? commandTimeout = null, params object[] commandParameters)
        {
            var obj = ExecuteScalar(commandText, commandType, commandTimeout, commandParameters);
            return (T)Convert.ChangeType(obj, typeof(T));
        }

        /// <summary>
        /// Execute SQL returning a single value using a new database connection
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public object ExecuteScalar(string commandText, CommandType commandType = CommandType.Text, int? commandTimeout = null, params object[] commandParameters)
        {
            using (var dbConnection = GetDbConnection())
            {
                return ExecuteScalar(dbConnection, commandText, commandType, commandTimeout, commandParameters);
            }
        }

        #endregion

        public DbDataReader ExecuteReader(DbConnection dbConnection, string commandText, CommandType commandType = CommandType.Text, int? commandTimeout = null, params object[] commandParameters)
        {
            var cmd = CreateCommand(dbConnection, commandText, commandType, commandTimeout, commandParameters);
            return cmd.ExecuteReader();
        }

        public IEnumerable<T> EnumerateReader<T>(DbDataReader dataReader) where T:IParseDataRecord, new()
        {
            return dataReader.Cast<IDataRecord>().Select(x => {
                var t = new T();
                t.ParseDataRecord(x);
                return t;
            });
        }

        #region ExecuteCommandAsEnumerable

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbCommand"></param>
        /// <param name="convertor"></param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteCommandAsEnumerable<T>(DbCommand dbCommand, Func<IDataRecord, T> convertor)
        {
            return ExecuteCommandAsEnumerable(dbCommand).Select(r => convertor(r));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbCommand"></param>
        /// <returns></returns>
        public IEnumerable<IDataRecord> ExecuteCommandAsEnumerable(DbCommand dbCommand)
        {
            using (var rdr = dbCommand.ExecuteReader())
            {
                foreach (IDataRecord rec in rdr.Cast<IDataRecord>())
                {
                    yield return rec;
                }
            }
        }

        #endregion

        #region ExecuteReaderAsEnumerable



        public IEnumerable<IDataRecord> ExecuteReaderAsEnumerable(string commandText, params DbParameter[] commandParameters)
        {
            return ExecuteReaderAsEnumerable(commandText, CommandType.Text, null, commandParameters);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public IEnumerable<IDataRecord> ExecuteReaderAsEnumerable(string commandText, CommandType commandType = CommandType.Text, int? commandTimeout = null, params object[] commandParameters)
        {
            using (var dbc = GetDbConnection())
            {
                foreach (var rec in ExecuteReaderAsEnumerable(dbc, commandText, commandType, commandTimeout, commandParameters))
                {
                    yield return rec;
                }
            }
        }

        public IEnumerable<T> ExecuteReaderAsEnumerable<T>(DbConnection dbConnection, string commandText, CommandType commandType = CommandType.Text, int? commandTimeout = null, params object[] commandParameters) where T : IParseDataRecord, new()
        {
            foreach(var rec in ExecuteReaderAsEnumerable(dbConnection, commandText, commandType, commandTimeout, commandParameters))
            {
                var obj = new T();
                obj.ParseDataRecord(rec);
                yield return obj;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="convertor"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteReaderAsEnumerable<T>(Func<IDataRecord, T> convertor, string commandText, CommandType commandType = CommandType.Text, int? commandTimeout = null, params object[] commandParameters)
        {
            return ExecuteReaderAsEnumerable(commandText, commandType, commandTimeout, commandParameters).Select(r => convertor(r));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public IEnumerable<IDataRecord> ExecuteReaderAsEnumerable(DbConnection dbConnection, string commandText, CommandType commandType = CommandType.Text, int? commandTimeout = null, params object[] commandParameters)
        {
            var dbCommand = CreateCommand(dbConnection, commandText, commandType, commandTimeout, commandParameters);
            return ExecuteCommandAsEnumerable(dbCommand);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="convertor"></param>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteReaderAsEnumerable<T>(Func<IDataRecord, T> convertor, DbConnection dbConnection, string commandText, CommandType commandType = CommandType.Text, int? commandTimeout = null, params object[] commandParameters)
        {
            return ExecuteReaderAsEnumerable(dbConnection, commandText, commandType, commandTimeout, commandParameters).Select(r => convertor(r));
        }

        #endregion

        #endregion

        #region Experimental attribute based mapping

        public static T ConvertByDataMemberName<T>(IDataRecord rec) where T : new()
        {
            T result = new T();
            ParseByDataMemberName<T>(rec, result);
            return result;
        }
        public static void ParseByDataMemberName<T>(IDataRecord rec, T result)
        {
            //
            // Get all properties with a ColumnMap(name="...") attribute as we are using this for column mapping
            //
            var mappedProperties = typeof(T).GetProperties().Select(p => new {
                Name = (p.GetCustomAttributes(typeof(ColumnMapAttribute), false).FirstOrDefault() as ColumnMapAttribute)?.Name,
                DefaultValue = (p.GetCustomAttributes(typeof(ColumnDefaultAttribute), false).FirstOrDefault() as ColumnDefaultAttribute)?.Value,
                Property = p
            }).Where(p => p.Name != null);

            foreach (var prop in mappedProperties)
            {
                Type targetType = prop.Property.PropertyType;
                bool isNullableGeneric = targetType.IsGenericType && targetType.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
                Type targetNullableType = isNullableGeneric ? Nullable.GetUnderlyingType(targetType) : null;
                bool isValueType = targetType.IsValueType;

                if (!rec.Exists(prop.Name))
                {
                    // If it's not in the record just ignore it.
                    continue;
                }
                //
                // Get the corresponding value from the IDataRecord
                //
                var value = rec.GetValue(rec.GetOrdinal(prop.Name));
                //
                // Handle DbNull values
                //
                if (value == null || Convert.IsDBNull(value))
                {
                    if (prop.DefaultValue != null)
                    {
                        if (isNullableGeneric)
                        {
                            prop.Property.SetValue(result, Convert.ChangeType(prop.DefaultValue, targetNullableType));
                        }
                        else
                        {
                            prop.Property.SetValue(result, Convert.ChangeType(prop.DefaultValue, targetType));
                        }
                    }
                    else
                    {
                        if (isValueType)
                        {
                            // Value type so can only leave it set to its default value for the type as we can't handle nulls
                        }
                        else
                        {
                            prop.Property.SetValue(result, null);
                        }
                    }
                }
                else
                {
                    if (isNullableGeneric)
                    {
                        prop.Property.SetValue(result, Convert.ChangeType(value, targetNullableType));
                    }
                    else
                    {
                        prop.Property.SetValue(result, Convert.ChangeType(value, targetType));
                    }
                }

            }
        }


        #endregion
    }
}
