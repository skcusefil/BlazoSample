using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        public IDictionary<string,object> ComponentParameters { get; }
        public string PageName { get; }
        public string MenuText { get; }
        void CreatePluginsServices(IServiceCollection services);
    }
}
