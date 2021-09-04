using System;
using Ac682.Extensions.Logging.Console;
using Hyperai.Events;
using Spectre.Console;

namespace HyperaiShell.App.Logging.ConsoleFormatters
{
    public class EventArgsFormatter : IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type.IsAssignableTo(typeof(GenericEventArgs));
        }

        public Markup Format(object obj, Type type, string format = null)
        {
            return new Markup(obj.GetType().Name, new Style(Color.Yellow));
        }
    }
}
