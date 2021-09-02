using System;
using HyperaiShell.Foundation.Data;
using HyperaiShell.Foundation.Plugins;
using Microsoft.Extensions.Configuration;

namespace HyperaiShell.App.Plugins
{
    public sealed class PluginContext : IPluginContext
    {
        public PluginMeta Meta { get; internal set; }
        public Lazy<IConfiguration> Configuration { get; internal set; }
        public Lazy<IRepository> Repository { get; internal set; }
    }
}
