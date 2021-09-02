using System;
using HyperaiShell.Foundation.Authorization;

namespace HyperaiShell.App.Models
{
    public static class TicketBoxExtensions
    {
        public static void PutExpiryTicket(this TicketBox box, string name, DateTime expiration)
        {
            var ticket = new ExpiryTicket(name, expiration);
            box.Put(ticket);
        }

        public static void PutLimitedTicket(this TicketBox box, string name, int count)
        {
            var ticket = new LimitedUseTicket(name, count);
            box.Put(ticket);
        }

        public static void PutTicket(this TicketBox box, string name)
        {
            var ticket = new NormalTicket(name);
            box.Put(ticket);
        }

        public static void Remove(this TicketBox box, string ticketName)
        {
            box.Remove(box.FindSpecificTicket(ticketName));
        }
    }
}
