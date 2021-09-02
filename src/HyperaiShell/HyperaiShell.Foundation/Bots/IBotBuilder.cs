using System;

namespace HyperaiShell.Foundation.Bots
{
    public interface IBotBuilder
    {
        IBotBuilder Configure(Action<BotBase> configure);

        BotBase Build(IServiceProvider provider);
    }
}
