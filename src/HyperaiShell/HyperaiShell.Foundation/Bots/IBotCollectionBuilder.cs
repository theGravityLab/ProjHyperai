using System;

namespace HyperaiShell.Foundation.Bots
{
    public interface IBotCollectionBuilder
    {
        IBotBuilder Add<TBot>() where TBot : BotBase;

        BotCollection Build(IServiceProvider provider);
    }
}
