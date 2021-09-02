using System;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperai.Units
{
    public class UnitFactory
    {
        static UnitFactory()
        {
            var _ = new UnitFactory();
        }

        public UnitFactory()
        {
            Instance = this;
        }

        public static UnitFactory Instance { get; set; }

        public UnitBase CreateUnit(Type type, MessageContext context, IServiceProvider provider)
        {
            var unit = (UnitBase) ActivatorUtilities.CreateInstance(provider, type);
            unit.Context = context;
            return unit;
        }
    }
}
