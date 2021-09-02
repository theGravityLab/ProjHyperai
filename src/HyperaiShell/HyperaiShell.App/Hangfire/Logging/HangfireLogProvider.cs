using Hangfire.Logging;
using HyperaiShell.Foundation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HyperaiShell.App.Hangfire.Logging
{
    public class HangfireLogProvider : ILogProvider
    {
        public ILog GetLogger(string name)
        {
            return new HangfireLogger(Shared.Host.Services.GetRequiredService<ILogger<HangfireLogger>>());
        }
    }
}
