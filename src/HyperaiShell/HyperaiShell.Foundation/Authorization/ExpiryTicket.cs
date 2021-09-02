using System;

namespace HyperaiShell.Foundation.Authorization
{
    public class ExpiryTicket : TicketBase
    {
        public ExpiryTicket(string name, DateTime expiration) : base(name)
        {
            ExpirationDate = expiration;
        }

        public DateTime ExpirationDate { get; }

        public override bool Verify()
        {
            return ExpirationDate >= DateTime.Now;
        }
    }
}
