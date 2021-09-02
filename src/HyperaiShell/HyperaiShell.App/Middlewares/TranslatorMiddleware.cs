using System.Linq;
using Hyperai.Events;
using Hyperai.Messages;
using Hyperai.Messages.ConcreteModels;
using Hyperai.Middlewares;
using Hyperai.Services;

namespace HyperaiShell.App.Middlewares
{
    public class TranslatorMiddleware : IMiddleware
    {
        private readonly IMessageChainParser _parser;

        public TranslatorMiddleware(IMessageChainParser parser)
        {
            _parser = parser;
        }

        public bool Run(IApiClient sender, GenericEventArgs args)
        {
            if (args is MessageEventArgs msgEvent)
            {
                var text = string.Join(string.Empty, msgEvent.Message.OfType<Plain>().Select(x => x.Text));
                if (text.Length > 8 && (text.StartsWith("```\r") || text.StartsWith("```\n")) &&
                    (text.EndsWith("\r```") || text.EndsWith("\n```")))
                    try
                    {
                        var msg = _parser.Parse(text[4..^4].Trim());
                        msgEvent.Message = msg;
                    }
                    catch
                    {
                        // not proper hyper code
                    }
            }

            return true;
        }
    }
}
