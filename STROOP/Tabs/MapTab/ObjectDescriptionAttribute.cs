using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STROOP.Tabs.MapTab
{
    public class ObjectDescriptionAttribute : Attribute
    {
        public readonly string DisplayName;
        public readonly string Initializer;
        public ObjectDescriptionAttribute(string DisplayName)
        {
            this.DisplayName = DisplayName;
            this.Initializer = null;
        }
        public ObjectDescriptionAttribute(string DisplayName, string InitializerName)
        {
            this.DisplayName = DisplayName;
            this.Initializer = InitializerName;
        }
    }
}
