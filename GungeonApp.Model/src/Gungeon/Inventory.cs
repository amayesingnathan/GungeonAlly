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
        public ItemBase[] Items { get; set; }

        public Inventory()
        {
            Items = new ItemBase[0];
        }

        public bool HasSynergy(Synergy trySynergy)
        {
            return trySynergy.RequireAll.All(x => Items.Any(y => x.BaseID == y.BaseID)) &&
                trySynergy.RequireOne.Any(x => Items.Any(y => x.BaseID == y.BaseID));
        }
    }
}