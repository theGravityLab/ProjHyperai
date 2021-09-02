namespace HyperaiShell.Foundation.Authorization
{
    public class NormalTicket : TicketBase
    {
        public NormalTicket(string name) : base(name)
        {
        }

        public override bool Verify()
        {
            return true;
        }
    }
}
