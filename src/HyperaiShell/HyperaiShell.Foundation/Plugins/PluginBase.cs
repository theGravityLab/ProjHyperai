using System;
using HyperaiShell.Foundation.Bots;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HyperaiShell.Foundation.Plugins
{
    public abstract class PluginBase
    {
        public virtual IPluginContext Context { get; set; }

        public abstract void ConfigureBots(IBotCollectionBuilder bots, IConfiguration config);

        public abstract void ConfigureServices(IServiceCollection services);

        public abstract void OnStarted(IServiceProvider provider, IConfiguration config);
    }
}
