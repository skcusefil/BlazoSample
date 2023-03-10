using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Plugin.Shared
{
    public class PluginService : IPluginService
    {
        public IEnumerable<Type> Components { get; private set; }
        public PluginService(string assemblyDir)
        {
            LoadComponents(assemblyDir);
        }

        public IEnumerable<IPluginComponent> GetComponents()
        {
            return Components.Select(x => (IPluginComponent)Activator.CreateInstance(x)).ToList();
        }

        public IPluginComponent GetComponentByPageName(string pageName)
        {
            return Components.Select(x => (IPluginComponent)Activator.CreateInstance(x))
            .SingleOrDefault(x => x.PageName == pageName);
        }

        #region private

        //Load Components
        private void LoadComponents(string path)
        {
            var components = new List<Type>();
            var assemblies = LoadAssemblies(path);

            foreach (var asm in assemblies)
            {
                var types = GetTypesWithInterface(asm);
                foreach (var typ in types) components.Add(typ);
            }

            Components = components;
        }

        //Load Assembly
        private IEnumerable<Assembly> LoadAssemblies(string path)
        {
            return Directory.GetFiles(path, "*.dll").Select(dll => Assembly.LoadFile(dll)).ToList();
        }

        //Get Assembly from Interface
        private IEnumerable<Type> GetTypesWithInterface(Assembly asm)
        {
            var it = typeof(IPluginComponent);
            return GetLoadableTypes(asm).Where(it.IsAssignableFrom).ToList();
        }

        //Get Loadable Types
        private IEnumerable<Type> GetLoadableTypes(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }

        #endregion
    }
}
