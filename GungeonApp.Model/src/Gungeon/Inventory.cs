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

        public void Add(Gun gun)
        {
            if (Items.ContainsKey(gun.BaseID))
            {
                Console.WriteLine("Inventory already contains this gun!");
                return;
            }

            Items[gun.BaseID] = gun;
            Guns[gun.BaseID] = gun;
        }
        public void Add(Item item)
        {
            if (Items.ContainsKey(item.BaseID))
            {
                Console.WriteLine("Inventory already contains this gun!");
                return;
            }


            switch (item.ItemTypeEnum)
            {
                case ItemTypes.Invalid:
                    Console.WriteLine("This is not a valid item type!");
                    return;

                case ItemTypes.Passive:
                    Passives[item.BaseID] = item;
                    break;

                case ItemTypes.Active:
                    Actives[item.BaseID] = item;
                    break;
            }

            Items[item.BaseID] = item;
        }

        public void Remove(int itemID)
        {
            Items.Remove(itemID);
            Guns.Remove(itemID);
            Passives.Remove(itemID);
            Actives.Remove(itemID);
        }

        public bool HasSynergy(Synergy trySynergy)
        {
            return trySynergy.RequireAll.All(x => Items.Any(y => x.BaseID == y.Key)) &&
                trySynergy.RequireOne.Any(x => Items.Any(y => x.BaseID == y.Key));
        }
    }
}