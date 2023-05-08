using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GungeonAlly.Model
{
    public enum LoadType
    {
        Default, 
        ImageUrl,
        QualityURL
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class DataNameAttribute : Attribute
    {
        protected string _valueName { get; set; }
        protected LoadType _loadType { get; set; }

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
        public LoadType LoadType
        {
            get
            {
                return _loadType;
            }
            set
            {
                _loadType = value;
            }
        }

        public DataNameAttribute()
        {
            _valueName = string.Empty;
            _loadType = LoadType.Default;
        }

        public DataNameAttribute(string valueName)
        {
            _valueName = valueName;
            _loadType = LoadType.Default;
        }

        public DataNameAttribute(string valueName, LoadType loadType)
        {
            _valueName = valueName;
            _loadType = loadType;
        }
    }
}
