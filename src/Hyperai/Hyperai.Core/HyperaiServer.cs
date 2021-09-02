using System;
using System.Threading;
using System.Threading.Tasks;
using Hyperai.Events;
using Hyperai.Middlewares;
using Hyperai.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hyperai
{
    public class HyperaiServer : IHostedService
    {
        private readonly IApiClient _client;
        private readonly HyperaiServerOptions _options;
        private readonly IServiceProvider _provider;

        public HyperaiServer(IApiClient client, IServiceProvider provider, HyperaiServerOptions options)
        {
            _client = client;
            _provider = provider;
            _options = options;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                _client.Connect();
                _client.On<GenericEventArgs>((sender, args) =>
                {
                    using var scope = _provider.CreateScope();
                    foreach (var type in _options.Middlewares)
                    {
                        var middleware =
                            ActivatorUtilities.CreateInstance(_provider, type) as IMiddleware;
                        if (!middleware!.Run(sender, args)) break;
                    }
                });
                _client.Listen();
            }, cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _client.Disconnect();
            return Task.CompletedTask;
        }
    }
}
