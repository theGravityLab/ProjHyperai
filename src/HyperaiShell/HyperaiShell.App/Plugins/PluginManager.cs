using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HyperaiShell.App.Packages;
using HyperaiShell.Foundation.Plugins;

namespace HyperaiShell.App.Plugins
{
    public class PluginManager
    {
        public static PluginManager Instance { get; } = new();

        private PluginManager()
        {
            // PackageManager.Instance.AssemblyLoaded = SearchPluginBase;
            // 不应该挂钩子，应该在一个统一的时间点去遍历找 PluginBase
        }

        private IDictionary<Type, IPluginContext> plugins = new Dictionary<Type, IPluginContext>();

        public void RegisterPlugin(Type plugin, IPluginContext context)
        {
            if (!plugin.IsSubclassOf(typeof(PluginBase)))
                throw new ArgumentException("Not derives from PluginBase", nameof(plugin));
            
            plugins.Add(plugin, context);
        }

        public PluginBase Activate(Type type)
        {
            if (plugins.ContainsKey(type))
            {
                var plugin = (PluginBase) Activator.CreateInstance(type);
                plugin.Context = plugins[type];
                return plugin;
            }

            throw new InvalidOperationException("Argument type for a plugin has not registered yet.");
        }

        public void ActivateAll(Action<PluginBase> configure)
        {
            foreach (var type in GetManagedPlugins())
            {
                configure(Activate(type));
            }
        }

        public IEnumerable<Type> GetManagedPlugins() => plugins.Keys;

        public IPluginContext GetContextOfPlugin(Type plugin) =>
            plugins[plugin];
        
    }
}
