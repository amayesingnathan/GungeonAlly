using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GungeonApp.Model
{
    public static class GameEntryEnumerableExtensions
    {        public static IEnumerable<GameEntry> Match(this IEnumerable<GameEntry> self, string input)
        {
            return self.Where(x => x.Name.StartsWith(input));
        }
    }
}
