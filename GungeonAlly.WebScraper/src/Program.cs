using GungeonAlly.Database;
using System.Configuration;

namespace GungeonAlly.WebScraper
{
    class Program
    {
        static string ConnectionString = ConfigurationManager.ConnectionStrings["EtGDb"].ConnectionString;

        static void Main()
        {
            var gungeonDb = new GungeonDB(ConnectionString);
            gungeonDb.ResetDatabase();
            var webScraper = new Scraper(gungeonDb);

            gungeonDb.ImportGuns(webScraper.GetGunData());
            gungeonDb.ImportItems(webScraper.GetItemData());

            gungeonDb.ImportSynergies(webScraper.GetSynergiesData());
        }
    }
}
