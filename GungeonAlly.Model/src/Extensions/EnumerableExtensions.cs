using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GungeonAlly.Model
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> DeNull<T>(this IEnumerable<T?> enumerable)
        {
            return enumerable.Where(x => x is not null).Cast<T>();
        }
    }
}
