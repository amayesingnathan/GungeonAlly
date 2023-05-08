using System;
using System.Data;
using System.Linq;
using GungeonAlly.DatabaseCore.ColumnAttribute;

namespace GungeonAlly.DatabaseCore
{
    public static class IDataRecordExtensions
    {
        /// <summary>
        /// Retrieve the ordinal position of a column in the data record (case insensitive)
        /// </summary>
        /// <param name="dataRecord"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static int? GetOrdinalIgnoreCase(this IDataRecord dataRecord, string columnName)
        {
            var ret = Enumerable.Range(0, dataRecord.FieldCount).Where(x => dataRecord.GetName(x).Equals(columnName, StringComparison.OrdinalIgnoreCase));
            return ret.Count() > 0 ? (int?)ret.First() : null;
        }
        /// <summary>
        /// Check a field name exists in the record (case insensitive)
        /// </summary>
        /// <param name="dataRecord"></param>
        /// <param name="name">Name of field to test for</param>
        /// <returns></returns>
        public static bool Exists(this IDataRecord dataRecord, string columnName)
        {
            return GetOrdinalIgnoreCase(dataRecord, columnName) != null;
        }

        /// <summary>
        /// Parse a data record and populate the passed object using [ColumnMap] attributes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataRecord"></param>
        /// <param name="result"></param>
        public static void ParseByColumnMap<T>(this IDataRecord dataRecord, T result)
        {
            //
            // Get all properties with a ColumnMap(name="...") attribute as we are using this for column mapping
            // Also checks for [ColumnDefault] attribute to determine if a default value should be returned.
            //
            var mappedProperties = typeof(T).GetProperties().Select(p => new
            {
                Property = p,
                Name = (p.GetCustomAttributes(typeof(ColumnMapAttribute), false).FirstOrDefault() as ColumnMapAttribute)?.Name,
                DefaultValue = (p.GetCustomAttributes(typeof(ColumnDefaultAttribute), false).FirstOrDefault() as ColumnDefaultAttribute)?.Value
            }).Where(p => p.Name != null);

            foreach (var prop in mappedProperties)
            {
                //
                // Get the corresponding value from the IDataRecord
                //
                var ordinal = dataRecord.GetOrdinalIgnoreCase(prop.Name);
                var value = ordinal.HasValue ? dataRecord.GetValue(ordinal.Value) : null;

                try
                {
                    Type targetType = prop.Property.PropertyType;
                    bool isNullableGeneric = targetType.IsGenericType && targetType.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
                    Type targetNullableType = isNullableGeneric ? Nullable.GetUnderlyingType(targetType) : null;
                    bool isValueType = targetType.IsValueType;

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
                            if (targetType.IsEnum)
                            {
                                if (targetType.IsEnumDefined(value))
                                {
                                    prop.Property.SetValue(result, value);
                                }
                            }
                            else
                            {
                                prop.Property.SetValue(result, Convert.ChangeType(value, targetType));
                            }
                        }
                    }
                }
                catch (Exception fex)
                {
                    // Any exceptions get caught and wrapped with current column data
                    throw new ColumnMapParseException(prop.Name, value, fex);
                }
            }
        }

        public static T ConvertByColumnMap<T>(this IDataRecord dataRecord) where T : new()
        {
            T result = new T();
            ParseByColumnMap<T>(dataRecord, result);
            return result;
        }

        public static T GetValue<T>(this IDataRecord dataRecord, string name, object defaultValue = null) // where T : class
        {
            T result = default(T);

            Type targetType = typeof(T);
            bool isNullableGeneric = targetType.IsGenericType && targetType.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
            Type targetNullableType = isNullableGeneric ? Nullable.GetUnderlyingType(targetType) : null;
            bool isValueType = targetType.IsValueType;

            var ordinal = dataRecord.GetOrdinalIgnoreCase(name);
            //
            // Get the corresponding value from the IDataRecord
            //
            var value = ordinal.HasValue ? dataRecord.GetValue(ordinal.Value) : null;
            //
            // Handle DbNull values
            //
            if (value == null || Convert.IsDBNull(value))
            {
                if (defaultValue != null)
                {
                    if (isNullableGeneric)
                    {
                        result = (T)Convert.ChangeType(defaultValue, targetNullableType);
                    }
                    else
                    {
                        result = (T)Convert.ChangeType(defaultValue, targetType);
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
                        result = default(T);
                    }
                }
            }
            else
            {
                if (isNullableGeneric)
                {
                    result = (T)Convert.ChangeType(value, targetNullableType);
                }
                else
                {
                    result = (T)Convert.ChangeType(value, targetType);
                }
            }
            return result;
        }

    }
}
