using GungeonApp.Model;

namespace GungeonApp.WebApp.Services
{
    public class InventoryState
    {
        public Inventory CurrentInventory { get; set; } = new Inventory();
        public ItemBase? CurrentItemSelected { get; set; }
        public Synergy[] CurrentSynergies { get; set; } = new Synergy[0];

        public void ClearSelection()
        {
            CurrentItemSelected = null;
        }
    }
}
