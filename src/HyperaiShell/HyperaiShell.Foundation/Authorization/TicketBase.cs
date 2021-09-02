using System.Text.RegularExpressions;

namespace HyperaiShell.Foundation.Authorization
{
    public abstract class TicketBase
    {
        protected TicketBase(string name)
        {
            Name = name;
            Pattern = new Regex(Regex.Escape(name).Replace(@"\*\*", "[a-z0-9.]+").Replace(@"\*", "[a-z0-9]+"));
        }

        public string Name { get; }
        public Regex Pattern { get; }

        public abstract bool Verify();
    }
}
