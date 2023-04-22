using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GungeonApp.DatabaseCore;
using GungeonApp.DatabaseCore.ColumnAttribute;

namespace GungeonApp.Model
{
    public class ItemB
    {
        public int ID { get; set; }
        [DataName("Icon")]
        public string IconUrl { get; set; }
        [DataName("Name")]
        public string Name { get; set; }
        [DataName("Type")]
        public string Type { get; set; }
        [DataName("Quote")]
        public string Quote { get; set; }
        [DataName("Quality")]
        public string Quality { get; set; }
        [DataName("Effect")]
        public string Effect { get; set; }

        public ItemB()
        {
            IconUrl = string.Empty;
            Name = string.Empty;
            Type = string.Empty;
            Quote = string.Empty;
            Quality = string.Empty;
            Effect = string.Empty;
        }
    }

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
