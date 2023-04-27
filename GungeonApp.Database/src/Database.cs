using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using GungeonApp.DatabaseCore;
using GungeonApp.Model;

namespace GungeonApp.Database
{
    public static class GungeonDB
    {
        //private const string ConnectionString = "Server=.\SQLEXPRESS;Database=EtGDB;Trusted_Connection=True;";
        private const string ConnectionString = "Server=localhost,1433;Database=EtGDB;User Id=SA; Password=&UWlveec123";
        public static void ImportGuns(IEnumerable<Gun> guns)
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection(ConnectionString);

                using (var dbc = db.GetDbConnection())
                {
                    InsertToBaseItemTable(dbc, guns);
                    InsertToGunTable(dbc, guns);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }
        }
        public static void ImportItems(IEnumerable<Item> items)
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection(ConnectionString);

                using (var dbc = db.GetDbConnection())
                {
                    InsertToBaseItemTable(dbc, items);
                    InsertToItemTable(dbc, items);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }
        }

        public static Gun? GetGun(int id)
        {
            DatabaseConnection db = new DatabaseConnection(ConnectionString);

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
        public static Gun[] GetGun(string name)
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection(ConnectionString);

                using (var dbc = db.GetDbConnection())
                {
                    string commandString = $@"select * from dbo.BaseItems bi
                                              inner join Guns g on bi.BaseID=g.BaseID
                                              where bi.ItemName='{name}'
                                            ";
                    return db.ExecuteReaderAsEnumerable<Gun>(dbc, commandString).ToArray();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Gun[0];
            }
        }
        public static Item? GetItem(int id)
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection(ConnectionString);

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
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public static Item[] GetItem(string name)
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection(ConnectionString);

                using (var dbc = db.GetDbConnection())
                {
                    string commandString = $@"select * from dbo.BaseItems bi
                                              inner join Items i on bi.BaseID=i.BaseID
                                              where bi.ItemName='{name}'
                                            ";
                    return db.ExecuteReaderAsEnumerable<Item>(dbc, commandString).ToArray();
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                return new Item[0];
            }
        }

        public static Synergy[] GetSynergies(int id)
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection(ConnectionString);

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
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Synergy[0];
            }
        }

        public static ItemBase[] MatchItem(string itemName)
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection(ConnectionString);

                using (var dbc = db.GetDbConnection())
                {
                    string commandString = $"select * from dbo.BaseItems where ItemName like '%{itemName}%'";
                    return db.ExecuteReaderAsEnumerable<ItemBase>(dbc, commandString).ToArray();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new ItemBase[0];
            }
        }


        private static void InsertToBaseItemTable(DbConnection dbc, IEnumerable<ItemBase> items)
        {
            string commandString = $@"
                        insert into dbo.BaseItems
                            (BaseID, Type, IconImageData, ItemName, Quote, Quality)
                        values
                            (@ID, @Type, @Image, @Name, @Quote, @Quality);    
                    ";

            var cmd = dbc.CreateCommand(commandString);
            var id = new SqlParameter("@ID", SqlDbType.Int);
            var image = new SqlParameter("@Image", SqlDbType.VarBinary, -1) { IsNullable = true };
            var type = new SqlParameter("@Type", SqlDbType.Int);
            var name = new SqlParameter("@Name", SqlDbType.NVarChar, -1);
            var quote = new SqlParameter("@Quote", SqlDbType.NVarChar, -1);
            var qual = new SqlParameter("@Quality", SqlDbType.Int);
            cmd.AddParameters(id, image, type, name, qual, quote);
            cmd.Prepare();

            foreach (var item in items)
            {
                id.Value = item.BaseID;
                type.Value = ((int)item.Type);
                image.Value = item.ImageData;
                name.Value = item.ItemName;
                quote.Value = item.Quote;
                qual.Value = ((int)item.Quality);

                cmd.ExecuteNonQuery();
            }
        }

        private static void InsertToGunTable(DbConnection dbc, IEnumerable<Gun> guns)
        {
            string commandString = $@"
                        insert into dbo.Guns
                            (BaseID, Notes, GunType, DPS, MagSize, AmmoCap, Damage, FireRate, ReloadTime, ShotSpeed, Range, Force, Spread, Class)
                        values
                            (@ID, @Notes, @GunType, @DPS, @MagSize, @AmmoCap, @Damage, @FireRate, @ReloadTime, @ShotSpeed, @Range, @Force, @Spread, @Class);    
                    ";

            var cmd = dbc.CreateCommand(commandString);
            var id = new SqlParameter("@ID", SqlDbType.Int);
            var notes = new SqlParameter("@Notes", SqlDbType.NVarChar, -1);
            var guntype = new SqlParameter("@GunType", SqlDbType.NVarChar, -1);
            var dps = new SqlParameter("@DPS", SqlDbType.NVarChar, -1);
            var magsize = new SqlParameter("@MagSize", SqlDbType.NVarChar, -1);
            var ammocap = new SqlParameter("@AmmoCap", SqlDbType.NVarChar, -1);
            var dmg = new SqlParameter("@Damage", SqlDbType.NVarChar, -1);
            var firerate = new SqlParameter("@FireRate", SqlDbType.NVarChar, -1);
            var reltime = new SqlParameter("@ReloadTime", SqlDbType.NVarChar, -1);
            var shotspeed = new SqlParameter("@ShotSpeed", SqlDbType.NVarChar, -1);
            var range = new SqlParameter("@Range", SqlDbType.NVarChar, -1);
            var force = new SqlParameter("@Force", SqlDbType.NVarChar, -1);
            var spread = new SqlParameter("@Spread", SqlDbType.NVarChar, -1);
            var klass = new SqlParameter("@Class", SqlDbType.NVarChar, -1);
            cmd.AddParameters(id, notes, guntype, dps, magsize, ammocap, dmg, firerate, reltime, shotspeed, range, force, spread, klass);
            cmd.Prepare();

            foreach (var gun in guns)
            {
                id.Value = gun.BaseID;
                notes.Value = gun.Notes;
                guntype.Value = gun.GunType;
                dps.Value = gun.DPS;
                magsize.Value = gun.MagSize;
                ammocap.Value = gun.AmmoCap;
                dmg.Value = gun.Damage;
                firerate.Value = gun.FireRate;
                reltime.Value = gun.ReloadTime;
                shotspeed.Value = gun.ShotSpeed;
                range.Value = gun.Range;
                force.Value = gun.Force;
                spread.Value = gun.Spread;
                klass.Value = gun.Class;

                cmd.ExecuteNonQuery();
            }
        }

        private static void InsertToItemTable(DbConnection dbc, IEnumerable<Item> items)
        {
            string commandString = $@"
                        insert into dbo.Items
                            (BaseID, ItemEffect)
                        values
                            (@ID, @Effect);    
                    ";

            var cmd = dbc.CreateCommand(commandString);
            var id = new SqlParameter("@ID", SqlDbType.Int);
            var effect = new SqlParameter("@Effect", SqlDbType.NVarChar, -1);
            cmd.AddParameters(id, effect);
            cmd.Prepare();

            foreach (var item in items)
            {
                id.Value = item.BaseID;
                effect.Value = item.Effect;

                cmd.ExecuteNonQuery();
            }
        }
    }
}
