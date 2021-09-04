using System;
using HyperaiShell.Foundation.Data;
using HyperaiShell.Foundation.Plugins;

namespace HyperaiShell.App.Plugins
{
    public class PluginRepository<TPlugin> : IPluginRepository<TPlugin> where TPlugin : PluginBase
    {
        public Type BelongingTo => typeof(TPlugin);

        public IRepository Value =>
            PluginManager.Instance.GetContextOfPlugin(BelongingTo).Repository.Value;
    }
}
