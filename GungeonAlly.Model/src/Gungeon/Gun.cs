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
    public class Gun : ItemBase
    {
        [ColumnMap("Notes")]
        [DataName("Notes")]
        public string Notes { get; set; }

        [ColumnMap("GunType")]
        [DataName("Type")]
        public string GunType { get; set; }

        [DataName("DPS")]
        [ColumnMap("DPS")]
        public string DPS { get; set; }

        [ColumnMap("MagSize")]
        [DataName("Magazine Size")]
        public string MagSize { get; set; }

        [ColumnMap("AmmoCap")]
        [DataName("Ammo Capacity")]
        public string AmmoCap { get; set; }

        [ColumnMap("Damage")]
        [DataName("Damage")]
        public string Damage { get; set; }

        [ColumnMap("FireRate")]
        [DataName("FireRate")]
        public string FireRate { get; set; }

        [ColumnMap("ReloadTime")]
        [DataName("Reload Time")]
        public string ReloadTime { get; set; }
        [ColumnMap("ShotSpeed")]
        [DataName("Shot Speed")]
        public string ShotSpeed { get; set; }

        [ColumnMap("Range")]
        [DataName("Range")]
        public string Range { get; set; }

        [ColumnMap("Force")]
        [DataName("Force")]
        public string Force { get; set; }

        [ColumnMap("Spread")]
        [DataName("Spread")]
        public string Spread { get; set; }

        [ColumnMap("Class")]
        [DataName("Class")]
        public string Class { get; set; }
        public Gun()
        {
            Type = BaseItemType.Gun;
            Notes = string.Empty;
            DPS = string.Empty;
            MagSize = string.Empty;
            AmmoCap = string.Empty;
            Damage = string.Empty;
            FireRate = string.Empty;
            ReloadTime = string.Empty;
            GunType = string.Empty;
            ShotSpeed = string.Empty;
            Range = string.Empty;
            Force = string.Empty;
            Spread = string.Empty;
            Class = string.Empty;
        }
        public override void ParseDataRecord(IDataRecord dataRecord)
        {
            dataRecord.ParseByColumnMap(this);
        }
    }
}
