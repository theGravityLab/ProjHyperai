using Hyperai.Events;
using Hyperai.Middlewares;
using Hyperai.Services;
using HyperaiShell.Foundation.Services;
using Sentry;

namespace HyperaiShell.App.Middlewares
{
    public class BotMiddleware : IMiddleware
    {
        private readonly IHub _hub;
        private readonly IBotService _service;

        public BotMiddleware(IBotService service, IHub hub)
        {
            _service = service;
            _hub = hub;
        }

        public bool Run(IApiClient sender, GenericEventArgs args)
        {
            _service.PushAsync(args).Wait();
            return true;
        }
    }
}
