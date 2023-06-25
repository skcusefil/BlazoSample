using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Shared
{
    public static class ApplicationExtensions
    {
        public static PluginService ServiceInstance = new PluginService();
        public static IServiceCollection AddPluginServices(this IServiceCollection services)
        {
            services.AddSingleton<IPluginService>(ServiceInstance);
            ServiceInstance.CreatePluginsServices(services);
            return services;
        }

        public static IApplicationBuilder UsePluginMiddleware(this IApplicationBuilder builder)
        {
            builder.UseStaticFiles();
            var root = AppDomain.CurrentDomain.BaseDirectory;

            builder.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
                       Path.Combine(root, "Plugins")),
                RequestPath = "/Plugins",
                EnableDirectoryBrowsing = true
            });

            return builder;
        }
    }

}
