
namespace Plugin.Shared
{
    public interface IPluginService
    {
        IEnumerable<IPluginComponent> GetComponents();
        IPluginComponent GetComponentByPageName(string pageName);
    }
}
