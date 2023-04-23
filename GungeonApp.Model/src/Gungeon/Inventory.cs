using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GungeonApp.Model
{
    public class Inventory
    {
        public Dictionary<int, ItemBase> Items { get; set; } = new Dictionary<int, ItemBase>();
        public Dictionary<int, Gun> Guns { get; set; } = new Dictionary<int, Gun>();
        public Dictionary<int, Item> Actives { get; set; } = new Dictionary<int, Item>();
        public Dictionary<int, Item> Passives { get; set; } = new Dictionary<int, Item>();

        public bool HasSynergy(Synergy trySynergy)
        {
            return trySynergy.RequireAll.All(x => Items.Any(y => x.BaseID == y.Key)) &&
                trySynergy.RequireOne.Any(x => Items.Any(y => x.BaseID == y.Key));
        }
    }
}