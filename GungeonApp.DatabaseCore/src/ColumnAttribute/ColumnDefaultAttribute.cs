using System;

namespace GungeonApp.DatabaseCore.ColumnAttribute
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
