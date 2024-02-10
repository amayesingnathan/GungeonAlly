using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using GungeonAlly.Database;
using GungeonAlly.Model;
using System.Net.Http.Json;
using System.Xml.Linq;

namespace GungeonAlly.WebApp.Services
{
    public class GungeonService : IGungeonService
    {
        private GungeonDB _DB;

        public GungeonService(string connectionString)
        {
            _DB = new GungeonDB(connectionString);
        }   

        public Gun? GetGun(int id)
        {
            try
            {
                var gun = _DB.GetGun(id);
                if (gun is null)
                {
                    Console.WriteLine("Could not locate gun with id {0}", id);
                }

                return gun;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return null;
            }
        }

        public Gun[] GetGun(string name)
        {
            try
            {
                var gun = _DB.GetGun(name);
                if (gun is null)
                {
                    Console.WriteLine("Could not locate gun with name {0}", name);
                }

                return gun ?? new Gun[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return new Gun[0];
            }
        }

        public Item? GetItem(int id)
        {
            try
            {
                var item = _DB.GetItem(id);
                if (item is null)
                {
                    Console.WriteLine("Could not locate item with id {0}", id);
                }

                return item;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return null;
            }
        }

        public Item[] GetItem(string name)
        {
            try
            {
                var item = _DB.GetItem(name);
                if (item is null)
                {
                    Console.WriteLine("Could not locate item with name {0}", name);
                }

                return item ?? new Item[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return new Item[0];
            }
        }

        public ItemBase[] SearchItem(string name)
        {
            try
            {
                var item = _DB.MatchItem(name);
                if (item is null)
                {
                    Console.WriteLine("Could not locate any items matching name {0}", name);
                }

                return item ?? new ItemBase[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return new ItemBase[0];
            }
        }

        public Synergy[] GetSynergies(int itemID)
        {
            try
            {
                var synergies = _DB.GetSynergies(itemID);
                if (synergies is null)
                {
                    Console.WriteLine("Could not locate any synergies for item {0}", itemID);
                }

                return synergies ?? new Synergy[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return new Synergy[0];
            }
        }
    }
}
