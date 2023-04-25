using GungeonApp.Model;

namespace GungeonApp.WebApp.Services
{
    public interface IGungeonService
    {
        Gun? GetGun(int id);
        Task<Gun?> GetGunAsync(int id);
        Gun[]? GetGun(string name);
        Task<Gun[]?> GetGunAsync(string name);

        Item? GetItem(int id);
        Task<Item?> GetItemAsync(int id);
        Item[]? GetItem(string name);
        Task<Item[]?> GetItemAsync(string name);

        ItemBase[]? SearchItem(string name);
        Task<ItemBase[]?> SearchItemAsync(string name);
    }
}
