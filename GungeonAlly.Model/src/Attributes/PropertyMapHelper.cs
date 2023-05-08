using System;
using System.Data;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GungeonAlly.Model
{
    public class PropertyMapHelper
    {
        public static void Map(Type type, DataRow row, PropertyInfo prop, object entity)
        {
            string columnName = AttributeHelper.GetDataName(type, prop.Name);
            LoadType loadType = AttributeHelper.GetDataLoadType(type, prop.Name);

            if (!string.IsNullOrWhiteSpace(columnName)
                && row.Table.Columns.Contains(columnName))
            {
                var propertyValue = row[columnName];
                if (propertyValue != DBNull.Value)
                {
                    ParsePrimitive(prop, entity, row[columnName], loadType);
                    return;
                }
            }
        }
        private static void ParsePrimitive(PropertyInfo prop, object entity, object value, LoadType loadType)
        {
            if (loadType == LoadType.ImageUrl && Uri.IsWellFormedUriString(value.ToString(), UriKind.Absolute))
            {
                HttpClient wc = new HttpClient();
                try
                {
                    byte[] data = wc.GetByteArrayAsync(value.ToString()).Result;
                    prop.SetValue(entity, data, null);
                }
                catch
                {
                    prop.SetValue(entity, null, null);
                }
            }
            else if (loadType == LoadType.QualityURL)
            {
                var url = value?.ToString()?.Split('/');

                if (url is null || url.Length < 8)
                {
                    return;
                }

                var qualityStr = url[7].Split('_').First().ToString();
                if (qualityStr.Length > 1)
                {
                    qualityStr = qualityStr[1].ToString();
                }

                Quality qual;
                if (Enum.TryParse(qualityStr, out qual))
                {
                    prop.SetValue(entity, qual, null);
                }

            }
            else if (prop.PropertyType == typeof(string))
            {
                prop.SetValue(entity, value.ToString()?.Trim(), null);
            }
            else if (prop.PropertyType == typeof(char))
            {
                if (value == null)
                {
                    prop.SetValue(entity, null, null);
                }
                else
                {
                    prop.SetValue(entity, value.ToString()?[0], null);
                }
            }
            else if (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?))
            {
                if (value == null)
                {
                    prop.SetValue(entity, null, null);
                }
                else
                {
                    prop.SetValue(entity, ParseBoolean(value.ToString()), null);
                }
            }
            else if (prop.PropertyType == typeof(long))
            {
                prop.SetValue(entity, long.Parse(value.ToString()), null);
            }
            else if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
            {
                if (value == null)
                {
                    prop.SetValue(entity, null, null);
                }
                else
                {
                    prop.SetValue(entity, int.Parse(value.ToString()), null);
                }
            }
            else if (prop.PropertyType == typeof(decimal))
            {
                prop.SetValue(entity, decimal.Parse(value.ToString()), null);
            }
            else if (prop.PropertyType == typeof(double) || prop.PropertyType == typeof(double?))
            {
                double number;
                bool isValid = double.TryParse(value.ToString(), out number);
                if (isValid)
                {
                    prop.SetValue(entity, double.Parse(value.ToString()), null);
                }
            }
            else if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(Nullable<DateTime>))
            {
                DateTime date;
                bool isValid = DateTime.TryParse(value.ToString(), out date);
                if (isValid)
                {
                    prop.SetValue(entity, date, null);
                }
                else
                {
                    isValid = DateTime.TryParseExact(value.ToString(), "MMddyyyy", new CultureInfo("en-US"), DateTimeStyles.AssumeLocal, out date);
                    if (isValid)
                    {
                        prop.SetValue(entity, date, null);
                    }
                }
            }
            else if (prop.PropertyType == typeof(Guid))
            {
                Guid guid;
                bool isValid = Guid.TryParse(value.ToString(), out guid);
                if (isValid)
                {
                    prop.SetValue(entity, guid, null);
                }
                else
                {
                    isValid = Guid.TryParseExact(value.ToString(), "B", out guid);
                    if (isValid)
                    {
                        prop.SetValue(entity, guid, null);
                    }
                }


            }
        }

        public static bool ParseBoolean(object value)
        {
            if (value == null || value == DBNull.Value) return false;

            switch (value.ToString()?.ToLowerInvariant())
            {
                case "1":
                case "y":
                case "yes":
                case "true":
                    return true;

                case "0":
                case "n":
                case "no":
                case "false":
                default:
                    return false;
            }
        }
    }
}
