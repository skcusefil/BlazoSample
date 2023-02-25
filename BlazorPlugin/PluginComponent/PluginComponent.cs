using Plugin.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginComponent
{
    public class PluginComponent : IPluginComponent
    {
        public Type ComponentType => typeof(PluginSample);

        public Dictionary<string, object> ComponentParameters => new Dictionary<string, object>();

        public string PageName => "pluginSample";

        public string ButtonText => "Plugin Sample";
    }
}
