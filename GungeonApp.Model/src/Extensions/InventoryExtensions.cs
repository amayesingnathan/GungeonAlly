using GungeonApp.Model.src.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GungeonApp.Model
{
    public static class InventoryExtensions
    {
        public static void AddRange(this Dictionary<int, Gun> guns, IEnumerable<Gun?>? gunsToAdd)
        {
            if (gunsToAdd == null)
                return;

            foreach (Gun gun in gunsToAdd.DeNull())
            {
                guns.Add(gun.BaseID, gun);
            }
        }
        public static void AddRange(this Dictionary<int, Item> items, IEnumerable<Item?>? itemsToAdd) 
        {
            if (itemsToAdd == null)
                return;

            foreach (Item item in itemsToAdd.DeNull()) 
            {
                items.Add(item.BaseID, item);
            }
        }
    }
}
