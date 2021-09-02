using System;
using Hyperai.Relations;
using HyperaiShell.Foundation.Authorization;

namespace HyperaiShell.Foundation.Services
{
    public static class AuthorizationServiceExtensions
    {
        public static void PutLimited(this IAuthorizationService service, RelationModel model, string name, int count)
        {
            var ticket = new LimitedUseTicket(name, count);
            service.PutTicket(model, ticket);
        }

        public static void PutExpiry(this IAuthorizationService service, RelationModel model, string name,
            DateTime expiration)
        {
            var ticket = new ExpiryTicket(name, expiration);
            service.PutTicket(model, ticket);
        }

        public static void PutNormal(this IAuthorizationService service, RelationModel model, string name)
        {
            var ticket = new NormalTicket(name);
            service.PutTicket(model, ticket);
        }
    }
}
