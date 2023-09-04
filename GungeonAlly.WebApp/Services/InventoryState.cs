using GungeonAlly.Database;
using GungeonAlly.Model;

namespace GungeonAlly.WebApp.Services
{
    public class InventoryState
    {
        private static readonly Synergy[] NoSynergies = Array.Empty<Synergy>();
        public InventoryState(IGungeonService dbService) 
        { 
            GungeonDB = dbService;
        }
        private IGungeonService GungeonDB { get; set; }
        public Inventory CurrentInventory { get; set; } = new Inventory();
        public ItemBase? CurrentItemSelected { get; private set; }
        public Synergy[] CurrentSynergies { get; set; } = NoSynergies;

        public void Select(ItemBase? item)
        {
            if (item == null)
            {
                CurrentItemSelected = null;
                CurrentSynergies = NoSynergies;
                return;
            }

            switch (item.Type)
            {
                case BaseItemType.Item:
                    CurrentItemSelected = GungeonDB.GetItem(item.BaseID);
                    break;

                case BaseItemType.Gun:
                    CurrentItemSelected = GungeonDB.GetGun(item.BaseID);
                    break;
            }

            CurrentSynergies = GungeonDB.GetSynergies(CurrentItemSelected.BaseID);
        }
        public void ClearSelection()
        {
            CurrentItemSelected = null;
        }
    }
}
