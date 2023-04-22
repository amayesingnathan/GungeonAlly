using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GungeonApp.Model
{
    public class Gun
    {
        public int ID{ get; set; }
        [DataName("Icon")]
        public string IconUrl { get; set; }
        [DataName("Name")]
        public string Name { get; set; }
        [DataName("Notes")]
        public string Notes { get; set; }
        [DataName("Quote")]
        public string Quote { get; set; }
        [DataName("Quality")]
        public string Quality { get; set; }
        [DataName("Type")]
        public string Type { get; set; }
        [DataName("DPS")]
        public string DPS { get; set; }
        [DataName("Magazine Size")]
        public string MagSize { get; set; }
        [DataName("Ammo Capacity")]
        public string AmmoCap { get; set; }
        [DataName("Damage")]
        public string Damage { get; set; }
        [DataName("FireRate")]
        public string FireRate { get; set; }
        [DataName("Reload Time")]
        public string ReloadTime { get; set; }
        [DataName("Shot Speed")]
        public string ShotSpeed { get; set; }
        [DataName("Range")]
        public string Range { get; set; }
        [DataName("Force")]
        public string Force { get; set; }
        [DataName("Spread")]
        public string Spread { get; set; }
        [DataName("Class")]
        public string Class { get; set; }
        
        public Gun()
        {
            IconUrl = string.Empty;
            Name = string.Empty;
            Notes = string.Empty;
            Quote = string.Empty;
            Quality = string.Empty;
            Type = string.Empty;
            DPS = string.Empty;
            MagSize = string.Empty;
            AmmoCap = string.Empty;
            Damage = string.Empty;
            FireRate = string.Empty;
            ReloadTime = string.Empty;
            ShotSpeed = string.Empty;
            Range = string.Empty;
            Force = string.Empty;
            Spread = string.Empty;
            Class = string.Empty;
        }
    }

}
