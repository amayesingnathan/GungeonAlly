using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Data;

using HtmlAgilityPack;

using GungeonApp.Model;

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

        private static IEnumerable<T> ExtractDataFromHtml<T>(HtmlNodeCollection nodes) where T: class, new()
        {
            var table = new DataTable();

            IEnumerable<string> headers = nodes[0]
                .Elements("th")
                .Select(th => th.InnerText.Trim())
                .DeNull();

            foreach (var header in headers)
            {
                table.Columns.Add(header);
            }

            string[][] rows = nodes.Skip(1).Select(tr => tr
                .Elements("td")
                .Select(td =>
                {
                    if (td.InnerText.Length != 0)
                        return td.InnerText.Trim();

                    return td.SelectSingleNode("a")?.SelectSingleNode("img")?.GetAttributeValue("data-src", string.Empty);
                })
                .DeNull()
                .ToArray())
                .ToArray();

            foreach (string[] row in rows)
            {
                table.Rows.Add(row);
            }

            var data = DataNameMapper<T>.Map(table);
            foreach (var row in data)
            {
                var prop = typeof(T).GetProperty("ID");
                prop?.SetValue(row, NextID++);
            }

            return data;
        }
    }
}
