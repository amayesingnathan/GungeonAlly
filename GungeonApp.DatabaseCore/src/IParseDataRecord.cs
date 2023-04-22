using System.Data;

namespace GungeonApp.DatabaseCore
{
    public interface IParseDataRecord
    {
        void ParseDataRecord(IDataRecord dataRecord);
    }
}
