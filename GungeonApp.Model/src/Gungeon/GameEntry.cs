using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GungeonApp.Model
{
    public enum GameEntryType
    {
        Gun, Item
    }
    public class GameEntry
    {
        public int ID {  get; set; }
        public string Name { get; set; }
        public GameEntryType Type { get; set; }
        public string IconUrl { get; set; }
        public string Quality { get; set; }

        public GameEntry(Gun gun)
        {
            ID = gun.ID;
            Name = gun.Name;
            Type = GameEntryType.Gun;
            IconUrl = gun.IconUrl;
            Quality = gun.Quality;
        }
        public GameEntry(Item item)
        {
            ID = item.ID;
            Name = item.Name;
            Type = GameEntryType.Item;
            IconUrl = item.IconUrl;
            Quality = item.Quality;
        }
    }
}
