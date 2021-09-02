using System.Collections.Generic;
using System.Linq;
using Hyperai.Relations;
using HyperaiShell.App.Models;
using HyperaiShell.Foundation.Authorization;
using HyperaiShell.Foundation.ModelExtensions;
using HyperaiShell.Foundation.Services;
using Microsoft.Extensions.Configuration;

namespace HyperaiShell.App.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly string _daddy;

        public AuthorizationService(IConfiguration configuration)
        {
            _daddy = configuration["Application:Daddy"];
        }

        public bool CheckTicket(RelationModel model, string specificName)
        {
            if (specificName == "whoisyourdaddy" && _daddy != null && _daddy == model.Identity.ToString()) return true;

            var ticketBox = model.Retrieve<TicketBox>();
            if (ticketBox == null) return false;

            var pass = ticketBox.Check(specificName);
            model.Attach(ticketBox);
            return pass;
        }

        public void PutTicket(RelationModel model, TicketBase ticket)
        {
            using (model.For(out var ticketBox, () => new TicketBox()))
            {
                ticketBox.Put(ticket);
            }
        }

        public void RemoveTicket(RelationModel model, string name)
        {
            using (model.For(out TicketBox ticketBox))
            {
                if (ticketBox != null) ticketBox.Remove(name);
            }
        }

        public IEnumerable<TicketBase> GetTickets(RelationModel model)
        {
            var ticketBox = model.Retrieve<TicketBox>();
            if (ticketBox != null) return ticketBox.GetTickets();
            return Enumerable.Empty<TicketBase>();
        }
    }
}
