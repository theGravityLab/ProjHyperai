using Hyperai;

namespace HyperaiShell.App.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static HyperaiServerOptionsBuilder UseBots(this HyperaiServerOptionsBuilder app)
        {
            app.Use<BotMiddleware>();
            return app;
        }

        public static HyperaiServerOptionsBuilder UseTranslator(this HyperaiServerOptionsBuilder app)
        {
            app.Use<TranslatorMiddleware>();
            return app;
        }

        public static HyperaiServerOptionsBuilder UseLogging(this HyperaiServerOptionsBuilder app)
        {
            app.Use<LoggingMiddleware>();
            return app;
        }

        public static HyperaiServerOptionsBuilder UseBlacklist(this HyperaiServerOptionsBuilder app)
        {
            app.Use<BlockMiddleware>();
            return app;
        }
    }
}
