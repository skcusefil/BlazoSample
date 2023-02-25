using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Shared
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddPluginService(this IServiceCollection services,string pluginDir)
        {
            services.AddSingleton<IPluginService, PluginService>(_ => new PluginService(pluginDir));
            return services;
        }
    }
}
