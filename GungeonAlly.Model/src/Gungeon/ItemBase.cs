using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GungeonAlly.DatabaseCore;
using GungeonAlly.DatabaseCore.ColumnAttribute;

namespace GungeonAlly.Model
{
    public enum BaseItemType
    {
        None, Gun, Item
    }
    public enum Quality
    {
        N, D, C, B, A, S
    }
    public class ItemBase : IParseDataRecord
    {
        [ColumnMap("BaseID")]
        public int BaseID { get; set; }

        [ColumnMap("IconImageData")]
        [DataName("Icon", LoadType.ImageUrl)]
        public byte[] ImageData { get; set; }

        [ColumnMap("ItemName")]
        [DataName("Name")]
        public string ItemName { get; set; }

        [ColumnMap("Type")]
        public BaseItemType Type { get; set; }

        [ColumnMap("Quote")]
        [DataName("Quote")]
        public string Quote { get; set; }

        [ColumnMap("Quality")]
        [DataName("Quality", LoadType.QualityURL)]
        public Quality Quality { get; set; }

        [ColumnMap("Description")]
        public string Description { get; set; }

        public virtual void ParseDataRecord(IDataRecord dataRecord)
        {
            dataRecord.ParseByColumnMap(this);
        }

        public ItemBase()
        {
            BaseID = int.MaxValue;
            Type = BaseItemType.None;
            ImageData = new byte[0];
            ItemName = string.Empty;
            Quote = string.Empty;
            Quality = Quality.N;
            Description = string.Empty;
        }
        public ItemBase(Gun gun)
        {
            BaseID = gun.BaseID;
            ItemName = gun.ItemName;
            Type = BaseItemType.Gun;
            ImageData = gun.ImageData;
            Quality = gun.Quality;
            Quote = gun.Quote;
            Description = gun.Description;
        }
        public ItemBase(Item item)
        {
            BaseID = item.BaseID;
            ItemName = item.ItemName;
            Type = BaseItemType.Item;
            ImageData = item.ImageData;
            Quality = item.Quality;
            Quote = item.Quote;
            Description = item.Description;
        }
    }

    public class ItemComparer : IEqualityComparer<ItemBase>
    {
        public bool Equals(ItemBase? a, ItemBase? b)
        {
            if (a == null && b == null)
                return true;
            else if (a == null || b == null)
                return false;

            return a.BaseID == b.BaseID;
        }

        public int GetHashCode(ItemBase a)
        {
            return a.BaseID.GetHashCode();
        }
    } 
}
