using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GungeonAlly.DatabaseCore;
using GungeonAlly.DatabaseCore.ColumnAttribute;

namespace GungeonAlly.Model
{
    public enum ItemTypes
    {
        Passive, Active, Invalid
    }

    public class Item : ItemBase
    {
        [ColumnMap("ItemEffect")]
        [DataName("Effect")]
        public string Effect { get; set; }

        [ColumnMap("ItemType")]
        [DataName("Type")]
        public string ItemType { get; set; }

        public ItemTypes ItemTypeEnum
        {
            get 
            {
                ItemTypes result;
                if (!Enum.TryParse(ItemType, out result))
                {
                    return ItemTypes.Invalid;
                }
                return result;
            }
        }

        public Item()
        {
            Type = BaseItemType.Item;
            Effect = string.Empty;
            ItemType = string.Empty;
        }
        public override void ParseDataRecord(IDataRecord dataRecord)
        {
            dataRecord.ParseByColumnMap(this);
        }
    }
}
