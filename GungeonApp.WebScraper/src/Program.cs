using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Data;

using Newtonsoft.Json;

namespace GungeonApp.WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            var json = JsonConvert.SerializeObject(Scraper.GetGunData(), Formatting.Indented);

            using (StreamWriter file = new StreamWriter("guns.json"))
            {
                file.Write(json);
            }

            json = JsonConvert.SerializeObject(Scraper.GetItemData(), Formatting.Indented);

            using (StreamWriter file = new StreamWriter("items.json"))
            {
                file.Write(json);
            }
        }
    }
}
