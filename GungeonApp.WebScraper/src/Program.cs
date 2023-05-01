using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Data;

using Newtonsoft.Json;

using GungeonApp.Model;
using GungeonApp.Database;

namespace GungeonApp.WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!GungeonDB.IsDBBaseInitialised())
            {
                GungeonDB.ImportGuns(Scraper.GetGunData());
                GungeonDB.ImportItems(Scraper.GetItemData());
            }
        }
    }
}
