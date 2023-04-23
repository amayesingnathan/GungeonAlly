using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GungeonApp.DatabaseCore;
using GungeonApp.DatabaseCore.ColumnAttribute;

namespace GungeonApp.Model
{
    public enum ItemType
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

        public string ImageAsURI
        {
            get
            {
                var imagesrc = Convert.ToBase64String(ImageData);
                return string.Format("data:image/png;base64,{0}", imagesrc);
            }
        }

        [ColumnMap("ItemName")]
        [DataName("Name")]
        public string ItemName { get; set; }

        [ColumnMap("Type")]
        public ItemType Type { get; set; }

        [ColumnMap("Quote")]
        [DataName("Quote")]
        public string Quote { get; set; }

        [ColumnMap("Quality")]
        [DataName("Quality", LoadType.QualityURL)]
        public Quality Quality { get; set; }

        public void ParseDataRecord(IDataRecord dataRecord)
        {
            dataRecord.ParseByColumnMap(this);
        }

        public ItemBase()
        {
            BaseID = int.MaxValue;
            Type = ItemType.None;
            ImageData = new byte[0];
            ItemName = string.Empty;
            Quote = string.Empty;
            Quality = Quality.N;
        }
        public ItemBase(Gun gun)
        {
            BaseID = gun.BaseID;
            ItemName = gun.ItemName;
            Type = ItemType.Gun;
            ImageData = gun.ImageData;
            Quality = gun.Quality;
            Quote = gun.Quote;
        }
        public ItemBase(Item item)
        {
            BaseID = item.BaseID;
            ItemName = item.ItemName;
            Type = ItemType.Item;
            ImageData = item.ImageData;
            Quality = item.Quality;
            Quote = item.Quote;
        }
    }
}
