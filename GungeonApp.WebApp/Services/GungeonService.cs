using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using GungeonApp.Model;
using System.Net.Http.Json;
using System.Xml.Linq;

namespace GungeonApp.WebApp.Services
{
    public class GungeonService : IGungeonService
    {
        private const string BaseAddress = "http://localhost:5101";
        private readonly HttpClient _httpClient;

        public GungeonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Gun? GetGun(int id)
        {
            try
            {
                string endpoint = $"{BaseAddress}/gun/id/{id}";
                var gun = _httpClient.GetFromJsonAsync<Gun>(endpoint).Result;
                if (gun is null)
                {
                    Console.WriteLine("Could not retrieve resource at {0}", endpoint);
                }

                return gun;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return null;
            }
        }

        public async Task<Gun?> GetGunAsync(int id)
        {
            try
            {
                string endpoint = $"{BaseAddress}/gun/id/{id}";
                var gun = await _httpClient.GetFromJsonAsync<Gun>(endpoint);
                if (gun is null)
                {
                    Console.WriteLine("Could not retrieve resource at {0}", endpoint);
                }

                return gun;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return null;
            }
        }

        public Gun[]? GetGun(string name)
        {
            try
            {
                string endpoint = $"{BaseAddress}/gun/name/{name}";
                var gun = _httpClient.GetFromJsonAsync<Gun[]>(endpoint).Result;
                if (gun is null)
                {
                    Console.WriteLine("Could not retrieve resource at {0}", endpoint);
                }

                return gun;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return null;
            }
        }

        public async Task<Gun[]> GetGunAsync(string name)
        {
            try
            {
                string endpoint = $"{BaseAddress}/gun/name/{name}";
                var gun = await _httpClient.GetFromJsonAsync<Gun[]>(endpoint);
                if (gun is null)
                {
                    Console.WriteLine("Could not retrieve resource at {0}", endpoint);
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
                string endpoint = $"{BaseAddress}/item/id/{id}";
                var item = _httpClient.GetFromJsonAsync<Item>(endpoint).Result;
                if (item is null)
                {
                    Console.WriteLine("Could not retrieve resource at {0}", endpoint);
                }

                return item;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return null;
            }
        }

        public async Task<Item?> GetItemAsync(int id)
        {
            try
            {
                string endpoint = $"{BaseAddress}/item/id/{id}";
                var item = await _httpClient.GetFromJsonAsync<Item>(endpoint);
                if (item is null)
                {
                    Console.WriteLine("Could not retrieve resource at {0}", endpoint);
                }

                return item;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return null;
            }
        }

        public Item[]? GetItem(string name)
        {
            try
            {
                string endpoint = $"{BaseAddress}/item/name/{name}";
                var item = _httpClient.GetFromJsonAsync<Item[]>(endpoint).Result;
                if (item is null)
                {
                    Console.WriteLine("Could not retrieve resource at {0}", endpoint);
                }

                return item;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return null;
            }
        }

        public async Task<Item[]> GetItemAsync(string name)
        {
            try
            {
                string endpoint = $"{BaseAddress}/item/name/{name}";
                var item = await _httpClient.GetFromJsonAsync<Item[]>(endpoint);
                if (item is null)
                {
                    Console.WriteLine("Could not retrieve resource at {0}", endpoint);
                }

                return item ?? new Item[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return new Item[0];
            }
        }

        public ItemBase[]? SearchItem(string name)
        {
            try
            {
                string endpoint = $"{BaseAddress}/search/{name}";
                var items = _httpClient.GetFromJsonAsync<Item[]>(endpoint).Result;
                if (items is null)
                {
                    Console.WriteLine("Could not retrieve resource at {0}", endpoint);
                }

                return items;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return null;
            }
        }

        public async Task<ItemBase[]> SearchItemAsync(string name)
        {
            try
            {
                string endpoint = $"{BaseAddress}/search/{name}";
                var items = await _httpClient.GetFromJsonAsync<ItemBase[]>(endpoint);
                if (items is null)
                {
                    Console.WriteLine("Could not retrieve resource at {0}", endpoint);
                }

                return items ?? new ItemBase[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return new ItemBase[0];
            }
        }

        public async Task<Synergy[]> GetSynergiesAsync(int itemID)
        {
            try
            {
                string endpoint = $"{BaseAddress}/synergy/{itemID}";
                var synergies = await _httpClient.GetFromJsonAsync<Synergy[]>(endpoint);
                if (synergies is null)
                {
                    Console.WriteLine("Could not retrieve resource at {0}", endpoint);
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
