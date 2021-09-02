using Microsoft.Extensions.Configuration;

namespace HyperaiShell.Foundation.Plugins
{
    public interface IPluginConfiguration<TPlugin> : IPluginProperty<TPlugin, IConfiguration>
        where TPlugin : PluginBase
    {
    }
}
