using GungeonApp.Model;

namespace GungeonApp.WebApp.Services
{
    public interface IGungeonService
    {
        Task<Gun?> GetGunAsync(int id);
        Task<Gun[]> GetGunAsync(string name);

        Task<Item?> GetItemAsync(int id);
        Task<Item[]> GetItemAsync(string name);

        Task<ItemBase[]> SearchItemAsync(string name);

        Task<Synergy[]> GetSynergiesAsync(int itemID);
    }
}
