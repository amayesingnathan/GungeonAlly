using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GungeonApp.DatabaseCore;
using GungeonApp.DatabaseCore.ColumnAttribute;

namespace GungeonApp.Model
{

    public class Item : ItemBase
    {
        [ColumnMap("ItemEffect")]
        [DataName("Effect")]
        public string Effect { get; set; }

        public Item()
        {
            Effect = string.Empty;
        }
    }
}
