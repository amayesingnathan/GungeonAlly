using GungeonAlly.Database;

namespace GungeonAlly.WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            GungeonDB.ResetDatabase();

            GungeonDB.ImportGuns(Scraper.GetGunData());
            GungeonDB.ImportItems(Scraper.GetItemData());

            GungeonDB.ImportSynergies(Scraper.GetSynergiesData());
        }
    }
}
