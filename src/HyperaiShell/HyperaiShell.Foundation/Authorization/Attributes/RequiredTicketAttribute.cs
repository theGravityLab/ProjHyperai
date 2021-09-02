using System.Collections.Generic;
using System.Linq;
using Hyperai.Units;
using Hyperai.Units.Attributes;
using HyperaiShell.Foundation.ModelExtensions;

namespace HyperaiShell.Foundation.Authorization.Attributes
{
    public class RequiredTicketAttribute : FilterByAttribute
    {
        private const string MESSAGE = "Operation denied: ";

        /// <summary>
        ///     检查是否具有某个特定的 <see cref="TicketBase" />
        /// </summary>
        /// <param name="specificName">票据(不含通配符</param>
        public RequiredTicketAttribute(string specificName) : base(new CheckTicketFilter(new[] {specificName}),
            MESSAGE + specificName)
        {
        }

        /// <summary>
        ///     检查是否具有某个特定的 <see cref="TicketBase" />
        /// </summary>
        /// <param name="specificNames">票据(不含通配符)，多组取或</param>
        public RequiredTicketAttribute(params string[] specificNames) : base(new CheckTicketFilter(specificNames),
            MESSAGE + string.Join(',', specificNames))
        {
        }
    }

    internal class CheckTicketFilter : IFilter
    {
        private readonly IEnumerable<string> names;

        public CheckTicketFilter(IEnumerable<string> specificNames)
        {
            names = specificNames;
        }

        public bool Check(MessageContext context)
        {
            return names.Any(x => context.User.CheckPermission(x));
        }
    }
}
