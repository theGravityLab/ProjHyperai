using System;
using System.Collections.Generic;
using Ac682.Extensions.Logging.Console;
using Hyperai.Events;

namespace HyperaiShell.App.Logging.ConsoleFormatters
{
    public class EventArgsFormatter : IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type.IsAssignableTo(typeof(GenericEventArgs));
        }

        public IEnumerable<ColoredUnit> Format(object obj, Type type, string format = null)
        {
            return new[]
            {
                new ColoredUnit(obj.GetType().Name, ConsoleColor.Yellow)
            };
        }
    }
}
