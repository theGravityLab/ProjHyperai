using HyperaiShell.Foundation.Data;

namespace HyperaiShell.Foundation.Plugins
{
    public interface IPluginRepository<TPlugin> : IPluginProperty<TPlugin, IRepository> where TPlugin : PluginBase
    {
    }
}
