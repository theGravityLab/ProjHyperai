using Hyperai.Events;
using Hyperai.Middlewares;
using Hyperai.Services;
using Microsoft.Extensions.Logging;

namespace HyperaiShell.App.Middlewares
{
    public class LoggingMiddleware : IMiddleware
    {
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(ILogger<LoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public bool Run(IApiClient client, GenericEventArgs eventArgs)
        {
            switch (eventArgs)
            {
                case GroupMessageEventArgs args:
                    _logger.LogInformation("{ArgsType} received {Group}-{User}:\n{Message}", args, args.Group,
                        args.User, args.Message);
                    break;

                case FriendMessageEventArgs args:
                    _logger.LogInformation("{ArgsType} received {User}:\n{Message}", args, args.User, args.Message);
                    break;

                case GroupMemberMutedEventArgs args:
                    _logger.LogInformation("{ArgsType} received {Group}:\n{User} by {Operator} for {Duration}", args,
                        args.Group, args.Whom, args.Operator, args.Duration);
                    break;

                case GroupJoinedEventArgs args:
                    _logger.LogInformation("{ArgsType} received {Group}:\n{User} by {Operator}", args, args.Group,
                        args.Who, args.Operator);
                    break;

                case GroupMemberUnmutedEventArgs args:
                    _logger.LogInformation("{ArgsType} received {Group}:\n{User} by {Operator}", args, args.Group,
                        args.Whom, args.Operator);
                    break;

                case FriendRecallEventArgs args:
                    _logger.LogInformation("{ArgsType} received {Friend}\n{Message}", args, args.WhoseMessage,
                        args.MessageId);
                    break;

                case GroupRecallEventArgs args:
                    _logger.LogInformation("{ArgsType} received {Group}-{User} by {Operator}\n{Message}", args,
                        args.Group, args.WhoseMessage, args.Operator, args.MessageId);
                    break;

                default:
                    _logger.LogInformation("{ArgsType} received at {Time}", eventArgs, eventArgs.Time);
                    break;
            }

            return true;
        }
    }
}
