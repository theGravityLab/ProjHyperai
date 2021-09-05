using System;
using Ac682.Extensions.Logging.Console;
using Hyperai.Relations;
using Spectre.Console;

namespace HyperaiShell.App.Logging.ConsoleFormatters
{
    public class RelationFormatter : IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type.IsAssignableTo(typeof(RelationModel));
        }

        public string Format(object obj, Type type, string format = null)
        {
            var name = obj switch
            {
                Friend friend => friend.Nickname,
                Member member => member.DisplayName,
                Group group => group.Name,
                RelationModel model => model.Identity.ToString(),
                _ => obj.GetType().Name
            };
            return $"[purple]{name}[/][grey]([/][red]{((RelationModel)obj).Identity}[/][grey])[/]";
        }
    }
}
