using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;

namespace GungeonAlly.Model
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

        private const int PanelScaleWidth = 100;
        public static IEnumerable<IEnumerable<ItemBase>> IntoBuckets(this IEnumerable<ItemBase> self)
        {
            int currentRowWidth = 0;
            List<ItemBase> bucket = new List<ItemBase>();

            foreach (ItemBase item in self)
            {
                using (Image image = Image.Load(item.ImageData))
                {
                    currentRowWidth += image.Width;
                }

                if (currentRowWidth >= PanelScaleWidth)
                {
                    yield return bucket;

                    bucket.Clear();
                    currentRowWidth = 0;
                }

                bucket.Add(item);
            }

            yield return bucket;
        }
    }
}
