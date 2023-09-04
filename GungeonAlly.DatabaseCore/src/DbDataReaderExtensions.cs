using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace GungeonAlly.DatabaseCore
{
    /// <summary>
    /// 
    /// </summary>
    public static class DbDataReaderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbDataReader"></param>
        /// <returns></returns>
        public static IEnumerable<T> AsEnumerable<T>(this DbDataReader dbDataReader) where T : IParseDataRecord, new()
        {
            foreach (var dbdr in dbDataReader.Cast<IDataRecord>())
            {
                var t = new T();
                t.ParseDataRecord(dbdr);
                yield return t;
            }
            dbDataReader.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbDataReader"></param>
        /// <param name="convertor"></param>
        /// <returns></returns>
        public static IEnumerable<T> AsEnumerable<T>(this DbDataReader dbDataReader, Func<IDataRecord, T> convertor)
        {
            foreach (var dbdr in dbDataReader.Cast<IDataRecord>())
            {
                yield return convertor(dbdr);
            }
            dbDataReader.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbDataReader"></param>
        /// <returns></returns>
        public static IEnumerable<T> AsEnumerableByColumnMap<T>(this DbDataReader dbDataReader) where T : new()
        {
            foreach (var dbdr in dbDataReader.Cast<IDataRecord>())
            {
                yield return dbdr.ConvertByColumnMap<T>();
            }
            dbDataReader.Close();
        }
    }
}
