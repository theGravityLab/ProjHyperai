using System;
using System.Collections.Generic;
using HyperaiShell.Foundation.Bots;

namespace HyperaiShell.App.Bots
{
    public class BotCollectionBuilder : IBotCollectionBuilder
    {
        private readonly List<IBotBuilder> botBuilders = new();

        IBotBuilder IBotCollectionBuilder.Add<TBot>()
        {
            var builder = new BotBuilder(typeof(TBot));
            botBuilders.Add(builder);
            return builder;
        }

        public BotCollection Build(IServiceProvider provider)
        {
            var collection = new BotCollection();
            foreach (var builder in botBuilders) collection.Add(builder.Build(provider));
            return collection;
        }
    }
}
