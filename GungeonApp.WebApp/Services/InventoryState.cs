using GungeonApp.Model;

namespace GungeonApp.WebApp.Services
{
    public class InventoryState
    {
        public Inventory CurrentInventory { get; set; } = new Inventory();
        public ItemBase? CurrentItemSelected { get; set; }

        public void ClearSelection()
        {
            CurrentItemSelected = null;
        }
    }
}
