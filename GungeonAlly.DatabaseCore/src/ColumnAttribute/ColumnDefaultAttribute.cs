using System;

namespace GungeonAlly.DatabaseCore.ColumnAttribute
{
    public class ColumnDefaultAttribute : Attribute
    {
        public ColumnDefaultAttribute(object value)
        {
            Value = value;
        }
        public object Value { get; set; }
    }
}
