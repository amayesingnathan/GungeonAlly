using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;

namespace GungeonAlly.DatabaseCore
{
    public static class DbUtility
    {
        /// <summary>
        /// Helper function to return DbConnectionStringBuilder for the specified provider
        /// </summary>
        /// <param name="providerName">DB Provider Name</param>
        /// <returns></returns>
        public static DbConnectionStringBuilder GetConnectionStringBuilder(string providerName)
        {
            return DbProviderFactories.GetFactory(providerName).CreateConnectionStringBuilder();
        }
        
        /// <summary>
        /// Helper function to escape an identifier using the provided command builders catalog seperator and quote identifier functions
        /// </summary>
        /// <param name="commandBuilder"></param>
        /// <param name="args">Array of identifier to be combined</param>
        /// <returns></returns>
        public static string EscapeIdentifier(this DbCommandBuilder commandBuilder, params string[] args)
        {
            return string.Join(commandBuilder.CatalogSeparator,
                args.Select(x => commandBuilder.QuoteIdentifier(x)).ToArray()
                );
        }
        /// <summary>
        /// Overload the AddRange method to support IEnumerable type.
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="args"></param>
        public static void AddRange(this DbParameterCollection pc, IEnumerable<DbParameter> args)
        {
            pc.AddRange(args.ToArray());
        }
    }
}
