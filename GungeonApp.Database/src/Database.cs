using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GungeonApp.DatabaseCore;
using GungeonApp.Model;

namespace GungeonApp.Database
{
    public static class Database
    {
        public static Gun? GetGun(int id)
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection("EtGDB");

                using (var dbc = db.GetDbConnection())
                {
                    string commandString = $@"select * from dbo.BaseItems bi
                                              inner join Guns g on bi.BaseID=g.BaseID
                                              where bi.BaseID={id}
                                            ";
                    var results = db.ExecuteReaderAsEnumerable<Gun>(dbc, commandString);
                    return results.FirstOrDefault();
                }
            }
            catch
            {
                return null;
            }
        }
        public static Item? GetItem(int id)
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection("EtGDB");

                using (var dbc = db.GetDbConnection())
                {
                    string commandString = $@"select * from dbo.BaseItems bi
                                              inner join Items i on bi.BaseID=i.BaseID
                                              where bi.BaseID={id}
                                            ";
                    var results = db.ExecuteReaderAsEnumerable<Item>(dbc, commandString);
                    return results.FirstOrDefault();
                }
            }
            catch
            {
                return null;
            }
        }

        public static Synergy[] GetSynergies(int id)
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection("EtGDB");

                using (var dbc = db.GetDbConnection())
                {
                    string commandString = $@"select 
                                                s.SynergyID, 
                                                s.SynergyName, 
                                                s.SynergyEffect, 
                                                sd.RequireType, 
                                                bi.BaseID,
                                                bi.Type,
                                                bi.IconImageData,
                                                bi.ItemName,
                                                bi.Quote,
                                                bi.Quality
                                              from dbo.Synergies s
                                              inner join SynergyDetail sd on s.SynergyID = sd.SynergyID
                                              inner join BaseItems bi on bi.BaseID = sd.ItemID
                                              where sd.ItemID = {id}
                                            ";
                    var results = db.ExecuteReaderAsEnumerable<SynergyEntry>(dbc, commandString);

                    return results
                        .GroupBy(x => new { ID = x.SynergyID, Name = x.ItemName, Effect = x.Effect })
                        .Select(x => new Synergy
                        {
                            ID = x.Key.ID,
                            Name = x.Key.Name,
                            Effect = x.Key.Effect,
                            RequireAll = x.Where(y => y.RequireType == Requirement.RequireAll)
                                .Select(y => y as ItemBase)
                                .ToArray(),
                            RequireOne = x.Where(y => y.RequireType == Requirement.RequireOne)
                                .Select(y => y as ItemBase)
                                .ToArray()
                        })
                        .ToArray();
                }
            }
            catch
            {
                return new Synergy[0];
            }
        }

        public static ItemBase[] MatchItem(string itemName)
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection("EtGDB");

                using (var dbc = db.GetDbConnection())
                {
                    string commandString = $"select * from dbo.BaseItems where ItemName like %{itemName}%";
                    return db.ExecuteReaderAsEnumerable<ItemBase>(dbc, commandString).ToArray();
                }
            }
            catch
            {
                return new ItemBase[0];
            }
        }
    }
}
