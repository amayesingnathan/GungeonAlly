using GungeonApp.Model;

namespace GungeonApp.WebApp.Services
{
    public interface IGungeonService
    {
        Task<Gun?> GetGun(int id);
        Task<Gun[]?> GetGun(string name);

        Task<Item?> GetItem(int id);
        Task<Item[]?> GetItem(string name);

        Task<ItemBase[]?> SearchItem(string name);
    }
}
