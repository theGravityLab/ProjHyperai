using System;
using Hyperai.Services;

namespace Hyperai.Events
{
    public class DefaultEventHandler<T> : IEventHandler<T> where T : GenericEventArgs
    {
        private readonly Action<IApiClient, T> _action;
        private readonly IApiClient _client;

        public DefaultEventHandler(IApiClient client, Action<IApiClient, T> action)
        {
            _client = client;
            _action = action;
        }

        public void Handle(T args)
        {
            _action(_client, args);
        }
    }
}
