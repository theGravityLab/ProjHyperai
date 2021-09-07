using System;
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

        public string Format(object obj, Type type, string format = null)
        {
            return $"[yellow]{obj.GetType().Name}[/]";
        }
    }
}
