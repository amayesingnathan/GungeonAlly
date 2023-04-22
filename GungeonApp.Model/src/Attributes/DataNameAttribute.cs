using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GungeonApp.Model
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DataNameAttribute : Attribute
    {
        protected string _valueName { get; set; }

        public string ValueName
        {
            get
            {
                return _valueName;
            }
            set
            {
                _valueName = value;
            }
        }

        public DataNameAttribute()
        {
            _valueName = string.Empty;
        }

        public DataNameAttribute(string valueName)
        {
            _valueName = valueName;
        }
    }
}
