using System;
using System.Collections.Generic;
using Ac682.Extensions.Logging.Console;
using Hyperai.Messages;
using Hyperai.Messages.ConcreteModels;
using Spectre.Console;

namespace HyperaiShell.App.Logging.ConsoleFormatters
{
    public class MessageElementFormatter : IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type.IsAssignableTo(typeof(MessageElement));
        }

        public Markup Format(object obj, Type type, string format = null)
        {
            return new Markup(obj switch
            {
                Plain plain => $"[green]{plain.Text}[/]",
                MessageElement ele => $"[darkcyan]{ele}[/]",
                _ => $"[grey](UNKNOW)[/]"
            });
        }
    }
}
