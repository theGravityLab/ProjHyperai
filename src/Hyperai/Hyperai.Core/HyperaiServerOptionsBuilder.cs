using System;
using System.Collections.Generic;
using Hyperai.Middlewares;
using IBuilder;

namespace Hyperai
{
    public class HyperaiServerOptionsBuilder : IBuilder<HyperaiServerOptions>
    {
        private readonly List<Type> middlewares = new();

        public HyperaiServerOptions Build()
        {
            return new()
            {
                Middlewares = middlewares.AsReadOnly()
            };
        }

        public HyperaiServerOptionsBuilder Use(Type middleware)
        {
            if (!typeof(IMiddleware).IsAssignableFrom(middleware))
                throw new ArgumentException("Type should implements IMiddleware interface.");
            middlewares.Add(middleware);
            return this;
        }
    }
}
