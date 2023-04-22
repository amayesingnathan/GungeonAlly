using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GungeonApp.Model
{
    public static class StringEnumerableExtensions
    {
        public static IEnumerable<string> DeNull(this IEnumerable<string?> strings)
        {
            return strings.Select(s => s is null ? string.Empty : s);
        }
    }
}
