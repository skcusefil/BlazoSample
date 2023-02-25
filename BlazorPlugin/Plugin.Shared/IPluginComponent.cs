using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Shared
{
    public interface IPluginComponent
    {
        public Type ComponentType { get; }
        public Dictionary<string,object> ComponentParameters { get; }
        public string PageName { get; }
        public string ButtonText { get; }
    }
}
