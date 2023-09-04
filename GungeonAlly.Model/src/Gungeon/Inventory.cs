using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GungeonAlly.Model
{
    public class Inventory
    {
        private Dictionary<int, ItemBase> _Items = new Dictionary<int, ItemBase>();
        private Dictionary<int, Gun> _Guns = new Dictionary<int, Gun>();
        private Dictionary<int, Item> _Actives = new Dictionary<int, Item>();
        private Dictionary<int, Item> _Passives = new Dictionary<int, Item>();

        public IEnumerable<ItemBase> Items { get { return _Items.Values; }}
        public IEnumerable<Gun> Guns { get { return _Guns.Values; }}
        public IEnumerable<Item> Actives { get { return _Actives.Values; }}
        public IEnumerable<Item> Passives { get { return _Passives.Values; }}

        public void Add(ItemBase? item)
        {
            if (item is null)
                return;

            if (item is not Gun && item is not Item)
                return;

            if (_Items.ContainsKey(item.BaseID))
            {
                Console.WriteLine("Inventory already contains this item!");
                return;
            }


            switch (item.Type)
            {
                case BaseItemType.Gun:
                    AddGun(item as Gun);
                    break;

                case BaseItemType.Item:
                    AddItem(item as Item);
                    break;
            }
        }

        public void AddRange(IEnumerable<ItemBase> items)
        {
            foreach (ItemBase item in items)
            {
                Add(item);
            }
        }

        private void AddGun(Gun? gun)
        {
            if (gun == null)
                return;

            _Items[gun.BaseID] = gun;
            _Guns[gun.BaseID] = gun;
        }
        private void AddItem(Item? item)
        {
            if (item == null)
                return;

            switch (item.ItemTypeEnum)
            {
                case ItemTypes.Invalid:
                    Console.WriteLine("This is not a valid item type!");
                    return;

                case ItemTypes.Passive:
                    _Passives[item.BaseID] = item;
                    break;

                case ItemTypes.Active:
                    _Actives[item.BaseID] = item;
                    break;
            }

            _Items[item.BaseID] = item;
        }

        public void Remove(int itemID)
        {
            _Items.Remove(itemID);
            _Guns.Remove(itemID);
            _Passives.Remove(itemID);
            _Actives.Remove(itemID);
        }

        public bool HasSynergy(Synergy trySynergy)
        {
            bool requireAllMatch = trySynergy.RequireAll.All(x => _Items.Any(y => x.BaseID == y.Key));
            bool requireOneMatch = trySynergy.RequireOne.Any(x => _Items.Any(y => x.BaseID == y.Key)) || trySynergy.RequireOne.Length == 0;
            bool requireTwoMatch = trySynergy.RequireTwo.Count(x => _Items.Any(y => x.BaseID == y.Key)) >= 2 || trySynergy.RequireTwo.Length == 0;

            return requireAllMatch && requireOneMatch && requireTwoMatch;
        }
    }
}