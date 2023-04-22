using System;

namespace GungeonApp.DatabaseCore.ColumnAttribute
{
    public class ColumnMapAttribute : Attribute
    {
        public ColumnMapAttribute(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
