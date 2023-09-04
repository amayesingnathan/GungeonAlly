using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using GungeonAlly.DatabaseCore;
using GungeonAlly.Model;
using System.Xml.Linq;

namespace GungeonAlly.Database
{
    public static class GungeonDB
    {
        private const string ConnectionString = @"Server=.\SQLEXPRESS;Database=EtGDB;Trusted_Connection=True;";

        #region Database Initialisation
        public static void ResetDatabase()
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection(ConnectionString);

                using (var dbc = db.GetDbConnection())
                {
                    string commandString = @"
                        truncate table dbo.Items;
                        truncate table dbo.Guns;
                        truncate table dbo.SynergyDetail;
                        delete from dbo.Synergies;
                        delete from dbo.BaseItems;
                    ";
                    db.ExecuteNonQuery(commandString);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

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
        public static void ImportSynergies(IEnumerable<Synergy> synergies)
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection(ConnectionString);

                using (var dbc = db.GetDbConnection())
                {
                    InsertToSynergyTable(dbc, synergies);
                    InsertToSynergyDetailTable(dbc, synergies);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }
        }


        private static void InsertToBaseItemTable(DbConnection dbc, IEnumerable<ItemBase> items)
        {
            string commandString = $@"
                        insert into dbo.BaseItems
                            (BaseID, Type, IconImageData, ItemName, Quote, Quality, Description)
                        values
                            (@ID, @Type, @Image, @Name, @Quote, @Quality, @Description);    
                    ";

            var cmd = dbc.CreateCommand(commandString);
            var id = new SqlParameter("@ID", SqlDbType.Int);
            var image = new SqlParameter("@Image", SqlDbType.VarBinary, -1) { IsNullable = true };
            var type = new SqlParameter("@Type", SqlDbType.Int);
            var name = new SqlParameter("@Name", SqlDbType.NVarChar, -1);
            var quote = new SqlParameter("@Quote", SqlDbType.NVarChar, -1);
            var qual = new SqlParameter("@Quality", SqlDbType.Int);
            var desc = new SqlParameter("@Description", SqlDbType.NVarChar, -1);
            cmd.AddParameters(id, image, type, name, qual, quote, desc);
            cmd.Prepare();

            foreach (var item in items)
            {
                id.Value = item.BaseID;
                type.Value = ((int)item.Type);
                image.Value = item.ImageData;
                name.Value = item.ItemName;
                quote.Value = item.Quote;
                qual.Value = ((int)item.Quality);
                desc.Value = item.Description;

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
                            (BaseID, ItemEffect, ItemType)
                        values
                            (@ID, @Effect, @Type);    
                    ";

            var cmd = dbc.CreateCommand(commandString);
            var id = new SqlParameter("@ID", SqlDbType.Int);
            var effect = new SqlParameter("@Effect", SqlDbType.NVarChar, -1);
            var type = new SqlParameter("@Type", SqlDbType.NVarChar, -1);
            cmd.AddParameters(id, effect, type);
            cmd.Prepare();

            foreach (var item in items)
            {
                id.Value = item.BaseID;
                type.Value = item.ItemType;
                effect.Value = item.Effect;

                cmd.ExecuteNonQuery();
            }
        }

        private static void InsertToSynergyTable(DbConnection dbc, IEnumerable<Synergy> items)
        {
            string commandString = $@"
                        insert into dbo.Synergies
                            (SynergyID, Name, Effect)
                        values
                            (@ID, @Name, @Effect);    
                    ";

            var cmd = dbc.CreateCommand(commandString);
            var id = new SqlParameter("@ID", SqlDbType.Int);
            var name = new SqlParameter("@Name", SqlDbType.NVarChar, -1);
            var effect = new SqlParameter("@Effect", SqlDbType.NVarChar, -1);
            cmd.AddParameters(id, name, effect);
            cmd.Prepare();

            foreach (var item in items)
            {
                id.Value = item.ID;
                name.Value = item.Name;
                effect.Value = item.Effect;

                cmd.ExecuteNonQuery();
            }
        }

        private static void InsertToSynergyDetailTable(DbConnection dbc, IEnumerable<Synergy> synergies)
        {
            string commandString = $@"
                        insert into dbo.SynergyDetail
                            (SynergyID, ItemID, RequireType)
                        values
                            (@SynergyID, @ItemID, @Type);    
                    ";

            var cmd = dbc.CreateCommand(commandString);
            var synergyId = new SqlParameter("@SynergyID", SqlDbType.Int);
            var itemID = new SqlParameter("@ItemID", SqlDbType.Int);
            var type = new SqlParameter("@Type", SqlDbType.Int);
            cmd.AddParameters(synergyId, itemID, type);
            cmd.Prepare();

            foreach (var synergy in synergies)
            {
                foreach (var requireOne in synergy.RequireOne)
                {
                    synergyId.Value = synergy.ID;
                    itemID.Value = requireOne.BaseID;
                    type.Value = Requirement.RequireOne;

                    cmd.ExecuteNonQuery();
                }

                foreach (var requireOne in synergy.RequireTwo)
                {
                    synergyId.Value = synergy.ID;
                    itemID.Value = requireOne.BaseID;
                    type.Value = Requirement.RequireTwo;

                    cmd.ExecuteNonQuery();
                }

                foreach (var requireOne in synergy.RequireAll)
                {
                    synergyId.Value = synergy.ID;
                    itemID.Value = requireOne.BaseID;
                    type.Value = Requirement.RequireAll;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region ItemBase
        public static ItemBase[] GetAllItems()
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection(ConnectionString);

                using (var dbc = db.GetDbConnection())
                {
                    string commandString = "select * from dbo.BaseItems bi";
                    return db.ExecuteReaderAsEnumerable<ItemBase>(dbc, commandString).ToArray();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new ItemBase[0];
            }
        }

        public static ItemBase[] GetItemBase(string name)
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection(ConnectionString);

                using (var dbc = db.GetDbConnection())
                {
                    string commandString = $@"select * from dbo.BaseItems
                                              where ItemName='{SanitiseItemName(name)}'
                                            ";

                    return db.ExecuteReaderAsEnumerable<ItemBase>(dbc, commandString).ToArray();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new ItemBase[0];
            }
        }

        #endregion

        #region Gun
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
                                              where bi.ItemName='{SanitiseItemName(name)}'
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

        #endregion

        #region Item
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
                                              where bi.ItemName='{SanitiseItemName(name)}'
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

        #endregion

        #region Synergies
        public static Synergy[] GetSynergies(int id)
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection(ConnectionString);

                using (var dbc = db.GetDbConnection())
                {
                    string commandString = $@"select s.SynergyID, 
	                                            s.Name, 
	                                            s.Effect, 
	                                            sd.RequireType, 
	                                            bi.BaseID,
	                                            bi.Type,
	                                            bi.IconImageData,
	                                            bi.ItemName,
	                                            bi.Quote,
	                                            bi.Quality,
                                                bi.Description
                                            from
	                                            dbo.SynergyDetail sd
	                                            inner join dbo.Synergies s on s.SynergyID=sd.SynergyID		
	                                            inner join dbo.SynergyDetail osd on s.SynergyID=osd.SynergyID
	                                            inner join dbo.BaseItems bi on bi.BaseID=osd.ItemID
	                                            where sd.ItemID={id}
                                            ";
                    var results = db.ExecuteReaderAsEnumerable<SynergyEntry>(dbc, commandString);

                    return results
                        .GroupBy(x => new { ID = x.SynergyID, Name = x.SynergyName, x.Effect })
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
                                .ToArray(),
                            RequireTwo = x.Where(y => y.RequireType == Requirement.RequireTwo)
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

        #endregion

        #region Search
        public static ItemBase[] MatchItem(string itemName)
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection(ConnectionString);

                using (var dbc = db.GetDbConnection())
                {
                    string safeItemName = SanitiseItemName(itemName);
                    string commandString = @"
                        declare @SearchString nvarchar(max) = '%' + @Name + '%';
                        declare @GoodMatch    nvarchar(max) = '%' + @Name;
                        declare @BadMatch     nvarchar(max) = @Name + '%';

                        select TOP 50 * from dbo.BaseItems
                        where ItemName like @SearchString
                        ORDER BY 
                            CASE
                                WHEN ItemName LIKE @GoodMatch THEN 1
                                WHEN ItemName LIKE @BadMatch THEN 3
                                ELSE 2
                            END, ItemName
                    ";

                    var name = new SqlParameter("@Name", SqlDbType.NVarChar, -1) { Value = safeItemName };
                    return db.ExecuteReaderAsEnumerable<ItemBase>(dbc, commandString, CommandType.Text, null, name)
                        .OrderBy(x => !x.ItemName.StartsWith(itemName, StringComparison.OrdinalIgnoreCase))
                        .ThenBy(x => x.ItemName)
                        .ToArray();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new ItemBase[0];
            }
        }

        #endregion

        #region Utils

        public static void SetColumnValue(int id, string column, object value)
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection(ConnectionString);

                using (var dbc = db.GetDbConnection())
                {
                    string commandString = $@"
                                update dbo.BaseItems
                                    set {column} = @Value
                                where
                                    BaseID = @ID;    
                            ";

                    var cmd = dbc.CreateCommand(commandString);
                    var idParam = new SqlParameter("@ID", SqlDbType.Int) { Value = id };
                    var valueParam = new SqlParameter("@Value", SqlDbType.NVarChar, -1) { Value = value };
                    cmd.AddParameters(idParam, valueParam);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static string SanitiseItemName(string itemName)
        {
            return itemName.Replace("'", "''");
        }

        #endregion
    }
}
