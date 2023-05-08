using System.Data;

namespace GungeonAlly.DatabaseCore
{
    public interface IParseDataRecord
    {
        void ParseDataRecord(IDataRecord dataRecord);
    }
}
