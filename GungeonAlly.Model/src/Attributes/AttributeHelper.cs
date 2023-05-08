using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GungeonAlly.Model
{
    public class AttributeHelper
    {
        public static string GetDataName(Type type, string propertyName)
        {
            var property = type
                           .GetProperty(propertyName)
                           .GetCustomAttributes(false)
                           .Where(x => x.GetType().Name == "DataNameAttribute")
                           .FirstOrDefault();

            if (property != null)
            {
                return ((DataNameAttribute)property).ValueName;
            }
            return string.Empty;
        }
        public static LoadType GetDataLoadType(Type type, string propertyName)
        {
            var property = type
                           .GetProperty(propertyName)
                           .GetCustomAttributes(false)
                           .Where(x => x.GetType().Name == "DataNameAttribute")
                           .FirstOrDefault();

            if (property != null)
            {
                return ((DataNameAttribute)property).LoadType;
            }
            return LoadType.Default;
        }
    }
}
