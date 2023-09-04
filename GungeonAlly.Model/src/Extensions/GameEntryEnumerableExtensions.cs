using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GungeonAlly.Model
{
    public static class GameEntryEnumerableExtensions
    {        
        public static IEnumerable<ItemBase> Match(this IEnumerable<ItemBase> self, string input)
        {
            return self.Where(x => x.ItemName.StartsWith(input));
        }
    }
}
