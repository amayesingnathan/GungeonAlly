using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using GungeonApp.Model;

namespace GungeonApp.API
{
    public class GameDictionary
    {
        private GameEntry[] GameData { get; set; }
        private Dictionary<int, Gun> Guns { get; set; }
        private Dictionary<int, Item> Items { get; set; }

        public GameDictionary(string gunJson, string itemJson) 
        {
            Gun[]? guns = JsonConvert.DeserializeObject<Gun[]>(gunJson);
            Guns = guns?.ToDictionary(x => x.ID) ?? new Dictionary<int, Gun>();

            Item[]? items = JsonConvert.DeserializeObject<Item[]>(itemJson);
            Items = items?.ToDictionary(x => x.ID) ?? new Dictionary<int, Item>();

            GameData = Guns
                .Select(x => new GameEntry(x.Value))
                .Union(Items
                    .Select(x => new GameEntry(x.Value)))
                .ToArray();
        }

        public IEnumerable<Gun> FindGun(string name)
        {
            return GameData
                .Where(x => x.Name.Equals(name))
                .Select(x => Guns[x.ID]);
        }
        public IEnumerable<Item> FindItem(string name)
        {
            return GameData
                .Where(x => x.Name.Equals(name))
                .Select(x => Items[x.ID]);
        }

        public IEnumerable<GameEntry> Match(string input)
        {
            return GameData.Match(input);
        }
    }
}
