using System;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperai
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHyperaiServer(this IServiceCollection services,
            Action<HyperaiServerOptionsBuilder> configure)
        {
            var builder = new HyperaiServerOptionsBuilder();
            configure(builder);
            return services
                .AddSingleton(builder.Build())
                .AddHostedService<HyperaiServer>();
        }
    }
}
