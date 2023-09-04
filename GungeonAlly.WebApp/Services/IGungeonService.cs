using GungeonAlly.Model;

namespace GungeonAlly.WebApp.Services
{
    public interface IGungeonService
    {
        Gun? GetGun(int id);
        Gun[] GetGun(string name);

        Item? GetItem(int id);
        Item[] GetItem(string name);

        ItemBase[] SearchItem(string name);

        Synergy[] GetSynergies(int itemID);
    }
}
