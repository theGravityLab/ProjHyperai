using System;
using Hangfire.Logging;
using Microsoft.Extensions.Logging;
using LogLevel = Hangfire.Logging.LogLevel;

namespace HyperaiShell.App.Hangfire.Logging
{
    public class HangfireLogger : ILog
    {
        private readonly ILogger _logger;

        public HangfireLogger(ILogger logger)
        {
            _logger = logger;
        }

        public bool Log(LogLevel logLevel, Func<string> messageFunc, Exception exception = null)
        {
            var message = messageFunc?.Invoke();
            if (message == null) return true;
            _logger.Log(logLevel switch
            {
                LogLevel.Debug => Microsoft.Extensions.Logging.LogLevel.Debug,
                LogLevel.Info => Microsoft.Extensions.Logging.LogLevel.Information,
                LogLevel.Warn => Microsoft.Extensions.Logging.LogLevel.Warning,
                LogLevel.Error => Microsoft.Extensions.Logging.LogLevel.Error,
                LogLevel.Fatal => Microsoft.Extensions.Logging.LogLevel.Critical,
                LogLevel.Trace => Microsoft.Extensions.Logging.LogLevel.Trace,
                _ => Microsoft.Extensions.Logging.LogLevel.None
            }, exception, message);
            return true;
        }
    }
}
