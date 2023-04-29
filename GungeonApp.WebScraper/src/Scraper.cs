using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Data;

using HtmlAgilityPack;

using GungeonApp.Model;
using GungeonApp.Database;

namespace GungeonApp.WebScraper
{
    public class Scraper
    {
        public static int NextID = 0;
        public static IEnumerable<Gun> GetGunData()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://enterthegungeon.fandom.com/wiki/Guns");
            var nodes = doc.DocumentNode.SelectNodes("//table[@class='sortable mw-collapsible wikitable']//tr");

            return ExtractDataFromHtml<Gun>(nodes);
        }
        public static IEnumerable<Item> GetItemData()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://enterthegungeon.fandom.com/wiki/Items");
            var nodes = doc.DocumentNode.SelectNodes("//table[@class='wikitable sortable']//tr");

            return ExtractDataFromHtml<Item>(nodes);
        }
        public static void GetDescData()
        {
            var items = GungeonDB.GetAllItems();
            int i = 0; int max = items.Length; int percent = 0;

            foreach (ItemBase item in items)
            {
                if (++i % (max / 10) == 0)
                {
                    Console.WriteLine("Descriptions {0}% complete...", percent);
                    percent += 10;
                }

                ExtractDescriptionData(item);
                GungeonDB.SetColumnValue(item.BaseID, "Description", item.Description);
            }
        }

        private static IEnumerable<T> ExtractDataFromHtml<T>(HtmlNodeCollection nodes) where T: ItemBase, new()
        {
            var table = new DataTable();

            IEnumerable<string> headers = nodes[0]
                .Elements("th")
                .Select(th => th.InnerText.Replace("\\n", "").Trim())
                .ReplaceNull();

            foreach (var header in headers)
            {
                table.Columns.Add(header);
            }

            string[][] rows = nodes
                .Skip(1)
                .Select(tr => tr
                    .Elements("td")
                    .Select(td =>
                    {
                        var trimmed = td.InnerText
                            .Replace("\n", "")
                            .Replace("\r", "");

                        if (trimmed.Length != 0)
                            return trimmed.Trim();

                        return td.SelectSingleNode("a")?.SelectSingleNode("img")?.GetAttributeValue("data-src", string.Empty);
                    })
                    .ReplaceNull()
                    .ToArray())
                .ToArray();

            foreach (string[] row in rows)
            {
                table.Rows.Add(row);
            }

            var data = DataNameMapper<T>.Map(table)
                .Where(x => x.Quote.Length != 0);

            foreach (var row in data)
            {
                row.BaseID = NextID++;
            }

            return data;
        }
    
        private static void ExtractDescriptionData(ItemBase item)
        {
            string itemUrl = $"https://enterthegungeon.fandom.com/wiki/{item.ItemName.Replace(' ', '_')}";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(itemUrl);
            var desc = doc.DocumentNode.SelectSingleNode("//td[@class='ammonomicon-desc']");
            item.Description = desc.InnerText;
        }
    }
}
