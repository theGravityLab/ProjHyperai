using System.Collections.Generic;
using System.Linq;
using HyperaiShell.Foundation.Authorization;

namespace HyperaiShell.App.Models
{
    public class TicketBox
    {
        public List<TicketBase> Tickets { get; set; } = new();

        public bool Check(string name)
        {
            var mani = Tickets.Where(x => x.Pattern.Match(name).Success);
            var veri = false;
            var diposedTickets = new LinkedList<TicketBase>();
            foreach (var ticket in mani)
            {
                veri = ticket.Verify() || veri; // 不可短路
                if (!veri) diposedTickets.AddLast(ticket);
            }

            foreach (var ticket in diposedTickets) Tickets.Remove(ticket);
            return veri;
        }

        public void Put(TicketBase ticket)
        {
            Tickets.Add(ticket);
        }

        public void Remove(TicketBase ticket)
        {
            if (Tickets.Contains(ticket)) Tickets.Remove(ticket);
        }

        public TicketBase FindSpecificTicket(string ticketName)
        {
            return Tickets.FirstOrDefault(x => x.Name == ticketName);
        }

        public IEnumerable<TicketBase> GetTickets()
        {
            return Tickets;
        }
    }
}
