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

namespace GungeonAlly.Database
{
    public class GungeonDB
    {
        private string _ConnectionString;

        public GungeonDB(string connectionString)
        {
            _ConnectionString = connectionString;
        }


        #region Database Initialisation
        public void ResetDatabase()
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection(_ConnectionString);

                using (var dbc = db.GetDbConnection())
                {
                    dbc.ExecuteNonQuery("spResetAllTables", CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void ImportGuns(IEnumerable<Gun> guns)
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection(_ConnectionString);

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
        public void ImportItems(IEnumerable<Item> items)
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection(_ConnectionString);

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
        public void ImportSynergies(IEnumerable<Synergy> synergies)
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection(_ConnectionString);

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


        private void InsertToBaseItemTable(DbConnection dbc, IEnumerable<ItemBase> items)
        {
            var cmd = dbc.CreateCommand("spInsertBaseItem", CommandType.StoredProcedure);

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

        private void InsertToGunTable(DbConnection dbc, IEnumerable<Gun> guns)
        {
            var cmd = dbc.CreateCommand("spInsertGun", CommandType.StoredProcedure);

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

        private void InsertToItemTable(DbConnection dbc, IEnumerable<Item> items)
        {
            var cmd = dbc.CreateCommand("spInsertItem", CommandType.StoredProcedure);

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

        private void InsertToSynergyTable(DbConnection dbc, IEnumerable<Synergy> items)
        {
            var cmd = dbc.CreateCommand("spInsertSynergy", CommandType.StoredProcedure);

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

        private void InsertToSynergyDetailTable(DbConnection dbc, IEnumerable<Synergy> synergies)
        {
            var cmd = dbc.CreateCommand("spInsertSynergyDetail", CommandType.StoredProcedure);

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
        public ItemBase[] GetAllItems()
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection(_ConnectionString);

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

        public ItemBase[] GetItemBase(string name)
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection(_ConnectionString);

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
        public Gun? GetGun(int id)
        {
            DatabaseConnection db = new DatabaseConnection(_ConnectionString);

            using (var dbc = db.GetDbConnection())
            {
                const string commandString = "spGetGunById";
                return dbc.ExecuteReader(commandString, CommandType.StoredProcedure, parameterBinding: cmd => new[] { cmd.CreateParameter("@GunId", id) })
                    .AsEnumerable<Gun>()
                    .FirstOrDefault();
            }
        }
        public Gun[] GetGun(string name)
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection(_ConnectionString);

                using (var dbc = db.GetDbConnection())
                {
                    const string commandString = "spGetGunByName";

                    string safeItemName = SanitiseItemName(name);
                    return dbc.ExecuteReader(commandString, CommandType.StoredProcedure, parameterBinding: cmd => new[] { cmd.CreateParameter("@ItemName", safeItemName) })
                        .AsEnumerable<Gun>()
                        .ToArray();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Array.Empty<Gun>();
            }
        }

        #endregion

        #region Item
        public Item? GetItem(int id)
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection(_ConnectionString);

                using (var dbc = db.GetDbConnection())
                {
                    const string commandString = "spGetItemById";
                    return dbc.ExecuteReader(commandString, CommandType.StoredProcedure, parameterBinding: cmd => new[] { cmd.CreateParameter("@ItemId", id)})
                        .AsEnumerable<Item>()
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public Item[] GetItem(string name)
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection(_ConnectionString);

                using (var dbc = db.GetDbConnection())
                {
                    const string commandString = "spGetItemByName";

                    string safeItemName = SanitiseItemName(name);
                    return dbc.ExecuteReader(commandString, CommandType.StoredProcedure, parameterBinding: cmd => new[] { cmd.CreateParameter("@ItemName", safeItemName) })
                        .AsEnumerable<Item>()
                        .ToArray();
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                return Array.Empty<Item>();
            }
        }

        #endregion

        #region Synergies
        public Synergy[] GetSynergies(int id)
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection(_ConnectionString);

                using (var dbc = db.GetDbConnection())
                {
                    const string commandString = "spGetSynergies";
                    var results = dbc.ExecuteReader(commandString, CommandType.StoredProcedure, parameterBinding: cmd => new[] { cmd.CreateParameter("ItemId", id) })
                        .AsEnumerable<SynergyEntry>();

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
        public ItemBase[] MatchItem(string itemName)
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection(_ConnectionString);

                using (var dbc = db.GetDbConnection())
                {
                    const string commandString = "spMatchItemName";
                    string safeItemName = SanitiseItemName(itemName);

                    return dbc.ExecuteReader(commandString, CommandType.StoredProcedure, parameterBinding: cmd => new[] { cmd.CreateParameter("@Name", safeItemName) })
                        .AsEnumerable<ItemBase>()
                        .OrderBy(x => !x.ItemName.StartsWith(itemName, StringComparison.OrdinalIgnoreCase))
                        .ThenBy(x => x.ItemName)
                        .ToArray();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Array.Empty<ItemBase>();
            }
        }

        #endregion

        #region Utils

        public void SetColumnValue(int id, string column, object value)
        {
            try
            {
                DatabaseConnection db = new DatabaseConnection(_ConnectionString);

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
