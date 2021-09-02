using System.Collections.Generic;
using Hyperai.Relations;
using HyperaiShell.Foundation.Authorization;

namespace HyperaiShell.Foundation.Services
{
    public interface IAuthorizationService
    {
        void PutTicket(RelationModel model, TicketBase ticket);

        bool CheckTicket(RelationModel model, string specificName);

        void RemoveTicket(RelationModel model, string name);

        IEnumerable<TicketBase> GetTickets(RelationModel model);
    }
}
