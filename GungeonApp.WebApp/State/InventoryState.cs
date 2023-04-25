using GungeonApp.Model;

namespace GungeonApp.WebApp.State
{
    public class InventoryState
    {
        public Inventory CurrentInventory { get; set; } = new Inventory();
        public ItemBase? CurrentItemBaseSelected { get; set; }
        public Gun? CurrentGunSelected { get; set; }
        public Item? CurrentItemSelected { get; set; }
    }
}
