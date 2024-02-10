using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Data;

using HtmlAgilityPack;

using GungeonAlly.Model;
using GungeonAlly.Database;

namespace GungeonAlly.WebScraper
{
    public class Scraper
    {
        private GungeonDB _DB;
        private readonly HtmlWeb Web = new();
        private int NextItemID = 0;
        private int NextSynergyID = 0;

        public Scraper(GungeonDB db) { _DB = db; }

        public IEnumerable<Gun> GetGunData()
        {
            HtmlDocument doc = Web.Load("https://enterthegungeon.fandom.com/wiki/Guns");
            var nodes = doc.DocumentNode.SelectNodes("//table[@class='sortable mw-collapsible wikitable']//tr");

            return ExtractItemDataFromHtml<Gun>(nodes);
        }
        public IEnumerable<Item> GetItemData()
        {
            HtmlDocument doc = Web.Load("https://enterthegungeon.fandom.com/wiki/Items");
            var nodes = doc.DocumentNode.SelectNodes("//table[@class='wikitable sortable']//tr");

            return ExtractItemDataFromHtml<Item>(nodes);
        }
        public IEnumerable<Synergy> GetSynergiesData()
        {
            HtmlDocument doc = Web.Load("https://enterthegungeon.fandom.com/wiki/Synergies");
            var nodes = doc.DocumentNode.SelectSingleNode("//table[@class='wikitable sortable']//tbody").Elements("tr");

            return ExtractSynergyDataFromHtml(nodes);
        }

        private IEnumerable<T> ExtractItemDataFromHtml<T>(HtmlNodeCollection nodes) where T: ItemBase, new()
        {
            var table = new DataTable();

            IEnumerable<string> headers = nodes[0]
                .Elements("th")
                .Select(th => th.InnerText.ReplaceLineEndings("").Trim())
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
                        var trimmed = td.InnerText.ReplaceLineEndings("");

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

            int i = 0; int max = table.Rows.Count; int percent = 0;
            foreach (var row in data)
            {
                if (++i % (max / 10) == 0)
                {
                    Console.WriteLine("{0} Descriptions {1}% complete...", typeof(T).Name, percent);
                    percent += 10;
                }

                row.BaseID = NextItemID++;
                ExtractDescriptionData(row);
            }

            return data;
        }
    
        private void ExtractDescriptionData(ItemBase item)
        {
            string itemUrl = $"https://enterthegungeon.fandom.com/wiki/{item.ItemName.Replace(' ', '_')}";
            HtmlDocument doc = Web.Load(itemUrl);
            var desc = doc.DocumentNode.SelectSingleNode("//td[@class='ammonomicon-desc']");
            item.Description = desc.InnerText ?? string.Empty;
        }

        private IEnumerable<Synergy> ExtractSynergyDataFromHtml(IEnumerable<HtmlNode> nodes)
        {
            var synergyRows = nodes
                .Skip(1)
                .Select(tr => tr.Elements("td"));

            List<Synergy> synergies = new List<Synergy>();
            foreach (var row in synergyRows)
            {
                var synergy = new Synergy(); 
                synergy.ID = NextSynergyID++;
                synergy.Name = row.First()?.InnerText?.ReplaceLineEndings("") ?? string.Empty;
                synergy.Effect = row.Last()?.InnerText?.ReplaceLineEndings("") ?? string.Empty;

                bool hasSpriteColumn = synergy.Effect.Length == 0;
                if (hasSpriteColumn)
                    synergy.Effect = row.SkipLast(1).Last()?.InnerText?.ReplaceLineEndings("") ?? string.Empty;

                foreach (var synergyItems in row.Skip(1).SkipLast(hasSpriteColumn ? 2 : 1))
                {
                    var itemStrings = synergyItems.InnerText.Split("\n").Where(x => x.Length != 0);

                    Requirement requireType = Requirement.RequireAll;
                    if (itemStrings.First().Equals("One of the following:", StringComparison.OrdinalIgnoreCase))
                        requireType = Requirement.RequireOne;
                    if (itemStrings.First().Equals("Two of the following:", StringComparison.OrdinalIgnoreCase))
                        requireType = Requirement.RequireTwo;

                    if (requireType != Requirement.RequireAll)
                        itemStrings = itemStrings.Skip(1);

                    if (itemStrings.All(x => x.StartsWith("Master Round")))
                    {
                        // Special case for Chief Master and Master's Chambers synergies
                        var masterRounds = _DB.GetItemBase("Master Round");
                        if (synergy.Name.Equals("Chief Master"))
                            synergy.RequireTwo = masterRounds;
                        else if (synergy.Name.Equals("Master's Chambers"))
                            synergy.RequireOne = masterRounds;

                        continue;
                    }

                    GetSynergyItems(itemStrings, synergy, requireType);
                }

                synergies.Add(synergy);
            }

            return synergies;
        }

        private void GetSynergyItems(IEnumerable<string> items, Synergy synergy, Requirement type)
        {
            switch (type)
            {
                case Requirement.RequireOne:
                    synergy.RequireOne = items.Select(x => _DB.GetItemBase(x).First()).ToArray();
                    break;

                case Requirement.RequireTwo:
                    synergy.RequireTwo = items.Select(x => _DB.GetItemBase(x).First()).ToArray();
                    break;

                case Requirement.RequireAll:
                    synergy.RequireAll = items.Select(x => _DB.GetItemBase(x).First()).ToArray();
                    break;
            }
        }
    }
}
