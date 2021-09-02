using System;
using System.Collections.Generic;
using Ac682.Extensions.Logging.Console;
using Hyperai.Relations;

namespace HyperaiShell.App.Logging.ConsoleFormatters
{
    public class RelationFormatter : IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type.IsAssignableTo(typeof(RelationModel));
        }

        public IEnumerable<ColoredUnit> Format(object obj, Type type, string format = null)
        {
            return new[]
            {
                obj switch
                {
                    Friend friend => new ColoredUnit(friend.Nickname, ConsoleColor.DarkMagenta),
                    Member member => new ColoredUnit(member.DisplayName, ConsoleColor.DarkMagenta),
                    Group group => new ColoredUnit(group.Name, ConsoleColor.DarkMagenta),
                    _ => new ColoredUnit(((RelationModel) obj).Identity.ToString())
                },
                new ColoredUnit("(", ConsoleColor.DarkGray),
                new ColoredUnit(((RelationModel) obj).Identifier, ConsoleColor.Red),
                new ColoredUnit(")", ConsoleColor.DarkGray)
            };
        }
    }
}
