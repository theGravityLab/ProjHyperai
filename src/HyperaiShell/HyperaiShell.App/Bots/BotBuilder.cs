using System;
using HyperaiShell.Foundation.Bots;
using Microsoft.Extensions.DependencyInjection;

namespace HyperaiShell.App.Bots
{
    public class BotBuilder : IBotBuilder
    {
        private readonly Type _botType;
        private Action<BotBase> _configure;

        public BotBuilder(Type botType)
        {
            _botType = botType;
        }

        public BotBase Build(IServiceProvider provider)
        {
            var bot = (BotBase) ActivatorUtilities.CreateInstance(provider, _botType);
            _configure?.Invoke(bot);

            return bot;
        }

        public IBotBuilder Configure(Action<BotBase> configure)
        {
            _configure = configure;
            return this;
        }
    }
}
