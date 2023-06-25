using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace Plugin.Shared
{
    public class PluginService : IPluginService
    {
        public IEnumerable<IPluginComponent> PluginComponents { get; private set; }

        public string[] JavascriptPaths { get; private set; }

        public PluginService()
        {
            var rootDir = GetDomainRoot();

            string pluginDir = FindPluginRootFolder(rootDir);

            string[] pluginFolders = GetSubPluginFolders(pluginDir);

            var wwwroot = FindWWWRoot(pluginFolders);
            JavascriptPaths = GetJavascriptPaths(wwwroot);
            //get assembly from all plugin folders

            var pluginComponents = new List<IPluginComponent>();

            foreach (string pluginFolder in pluginFolders)
            {
                //Load Assembly
                var assemblies = Directory.GetFiles(pluginFolder, "*.dll").Select(dll => Assembly.LoadFile(dll)).ToList();

                foreach (var assembly in assemblies)
                {
                    //load assembly with context
                    PluginLoadContext loadContext = new PluginLoadContext(assembly.Location);
                    var pluginAssembly = loadContext.LoadFromAssemblyName(AssemblyName.GetAssemblyName(assembly.Location));
                    var plugin = CreatePlugin(pluginAssembly);
                    pluginComponents.AddRange(plugin);
                }
            }

            PluginComponents = pluginComponents;

        }

        public void CreatePluginsServices(IServiceCollection services)
        {
            foreach (var plugin in PluginComponents)
            {
                plugin.CreatePluginsServices(services);
            }
        }

        #region private

        //Get App Domain root
        private string GetDomainRoot()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        //Find plugin Folders
        private string FindPluginRootFolder(string rootDir)
        {
            return Directory.GetDirectories(rootDir).Where(x => x.Contains("Plugins")).FirstOrDefault();
        }

        private string[] GetSubPluginFolders(string pluginDir)
        {
            return Directory.GetDirectories(pluginDir);
        }

        private string FindWWWRoot(string[] folders)
        {
            string wwwroot = "";

            foreach (var folder in folders)
            {
                wwwroot = Directory.GetDirectories(folder).Where(x => x.Contains("wwwroot")).FirstOrDefault();
            }
            return wwwroot;
        }

        private string[] GetJavascriptPaths(string wwwroot)
        {
            return Directory.GetFiles(wwwroot, "*.js");

        }

        public IPluginComponent GetComponentByPageName(string pageName)
        {
            return PluginComponents.FirstOrDefault(x => x.PageName == pageName);
        }

        private IEnumerable<IPluginComponent> CreatePlugin(Assembly assembly)
        {
            int count = 0;

            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(IPluginComponent).IsAssignableFrom(type))
                {
                    IPluginComponent result = Activator.CreateInstance(type) as IPluginComponent;
                    if (result != null)
                    {
                        count++;
                        yield return result;
                    }
                }
            }
        }

        #endregion

    }
}
