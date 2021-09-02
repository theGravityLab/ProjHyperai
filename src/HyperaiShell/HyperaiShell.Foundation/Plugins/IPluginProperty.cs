using System;

namespace HyperaiShell.Foundation.Plugins
{
    public interface IPluginProperty<TPlugin, out TProperty> where TPlugin : PluginBase
    {
        Type Plugin => typeof(TPlugin);
        TProperty Value { get; }
    }
}
