namespace HyperaiShell.Foundation.Authorization
{
    public class LimitedUseTicket : TicketBase
    {
        public LimitedUseTicket(string name, int count) : base(name)
        {
            Count = count;
        }

        public int Count { get; private set; }

        public override bool Verify()
        {
            return Count-- > 0;
        }
    }
}
