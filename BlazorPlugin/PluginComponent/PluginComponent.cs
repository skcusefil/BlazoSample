using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Plugin.Shared;
using PluginComponent.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PluginComponent
{
    public class PluginComponent : IPluginComponent
    {
        public Type ComponentType => typeof(PluginSample);

        public IDictionary<string, object> ComponentParameters => 
            new Dictionary<string, object>()
            {
                { "JavascriptPaths", "exampleJsInterop.js" },
                {"IsPlugin", true }
            };

        public string PageName => "pluginSample";

        public string MenuText => "Plugin Sample";

        public void CreatePluginApplicationBuilder(IApplicationBuilder app)
        {
            app.UseStaticFiles();

        }

        public void CreatePluginsServices(IServiceCollection services)
        {
            services.AddScoped<ISomeComponentService, SomeComponentService>();  
        }
    }
}
