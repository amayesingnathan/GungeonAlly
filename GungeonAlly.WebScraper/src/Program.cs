using GungeonAlly.Database;

namespace GungeonAlly.WebScraper
{
    class Program
    {
        static void Main()
        {
            GungeonDB.ResetDatabase();
            var webScraper = new Scraper();

            GungeonDB.ImportGuns(webScraper.GetGunData());
            GungeonDB.ImportItems(webScraper.GetItemData());

            GungeonDB.ImportSynergies(webScraper.GetSynergiesData());
        }
    }
}
