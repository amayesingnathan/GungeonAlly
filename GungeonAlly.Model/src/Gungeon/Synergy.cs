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
    public class Synergy
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Effect { get; set; }
        public ItemBase[] RequireAll { get; set; }
        public ItemBase[] RequireOne { get; set; }
        public ItemBase[] RequireTwo { get; set; }
        public Synergy()
        {
            ID = int.MaxValue;
            Name = string.Empty;
            Effect = string.Empty;
            RequireAll = new ItemBase[0];
            RequireOne = new ItemBase[0];
            RequireTwo = new ItemBase[0];
        }
    }

    public enum Requirement
    {
        RequireAll, RequireOne, RequireTwo
    }
    public class SynergyEntry : ItemBase
    {
        [ColumnMap("SynergyID")]
        public int SynergyID { get; set; }
        [ColumnMap("Name")]
        public string SynergyName { get; set; }
        [ColumnMap("Effect")]
        public string Effect { get; set; }
        [ColumnMap("RequireType")]
        public Requirement RequireType { get; set; }

        public SynergyEntry()
        {
            SynergyID = int.MaxValue;
            SynergyName = string.Empty;
            Effect = string.Empty;
            RequireType = Requirement.RequireAll;
        }
        public override void ParseDataRecord(IDataRecord dataRecord)
        {
            dataRecord.ParseByColumnMap(this);
        }
    }
}