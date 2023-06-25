
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Plugin.Shared
{
    public interface IPluginService
    {        
        IEnumerable<IPluginComponent> PluginComponents { get; }
        IPluginComponent GetComponentByPageName(string pageName);
        void CreatePluginsServices(IServiceCollection services);

    }
}
