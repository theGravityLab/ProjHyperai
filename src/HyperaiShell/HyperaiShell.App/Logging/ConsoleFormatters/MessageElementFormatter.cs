using System;
using System.Collections.Generic;
using Ac682.Extensions.Logging.Console;
using Hyperai.Messages;
using Hyperai.Messages.ConcreteModels;

namespace HyperaiShell.App.Logging.ConsoleFormatters
{
    public class MessageElementFormatter : IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type.IsAssignableTo(typeof(MessageElement));
        }

        public IEnumerable<ColoredUnit> Format(object obj, Type type, string format = null)
        {
            return new[]
            {
                obj switch
                {
                    Plain plain => new ColoredUnit($"\"{plain.Text}\"", ConsoleColor.DarkGreen),
                    MessageElement ele => new ColoredUnit(ele.ToString(), ConsoleColor.DarkCyan),
                    _ => new ColoredUnit("UNKNOWN", ConsoleColor.DarkGray)
                }
            };
        }
    }
}
