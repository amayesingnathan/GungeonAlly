using GungeonApp.Model;

namespace GungeonApp.WebApp.State
{
    public enum DisplayType
    {
        Selection, AddInProgress
    }
    public class InventoryState
    {
        public DisplayType ViewType { get; set; } = DisplayType.Selection;
        public Inventory CurrentInventory { get; set; } = new Inventory();
        public ItemBase? CurrentItemSelected { get; set; }
        
        public void ClearSelection()
        {
            CurrentItemSelected = null;
        }
    }
}
