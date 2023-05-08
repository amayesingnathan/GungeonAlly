using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GungeonAlly.Model
{
    public class DataNameMapper<TEntity> where TEntity : class, new()
    {
        public static TEntity Map(DataRow row)
        {
            var columnNames = row.Table.Columns
                                  .Cast<DataColumn>()
                                  .Select(x => x.ColumnName)
                                  .ToList();

            //Step 2 - Get the Property Data Names
            var properties = (typeof(TEntity)).GetProperties()
                                              .Where(x => x.GetCustomAttributes(typeof(DataNameAttribute), true).Any())
                                              .ToList();

            //Step 3 - Map the data
            TEntity entity = new TEntity();
            foreach (var prop in properties)
            {
                PropertyMapHelper.Map(typeof(TEntity), row, prop, entity);
            }

            return entity;
        }

        public static IEnumerable<TEntity> Map(DataTable table)
        {
            //Step 1 - Get the Column Name
            var columnNames = table.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList();

            //Step 2 - Get the Property Data Names
            var properties = (typeof(TEntity)).GetProperties()
                                                .Where(x => x.GetCustomAttributes(typeof(DataNameAttribute), true).Any())
                                                .ToList();

            //Step 3 - Map the data
            int i = 0; int max = table.Rows.Count; int percent = 0;

            List<TEntity> entities = new List<TEntity>();
            foreach (DataRow row in table.Rows)
            {
                if (++i % (max / 10) == 0)
                {
                    Console.WriteLine("{0}s {1}% complete...", typeof(TEntity).Name, percent);
                    percent += 10;
                }

                var test = row[1] as string;
                TEntity entity = new TEntity();
                foreach (var prop in properties)
                {
                    PropertyMapHelper.Map(typeof(TEntity), row, prop, entity);
                }
                entities.Add(entity);
            }

            return entities;
        }
    }
}
