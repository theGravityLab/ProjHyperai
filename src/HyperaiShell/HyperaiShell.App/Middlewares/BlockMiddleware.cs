using Hyperai.Events;
using Hyperai.Middlewares;
using Hyperai.Services;
using HyperaiShell.Foundation.Services;
using Microsoft.Extensions.Logging;

namespace HyperaiShell.App.Middlewares
{
    public class BlockMiddleware : IMiddleware
    {
        private readonly ILogger _logger;
        private readonly IBlockService _service;

        public BlockMiddleware(IBlockService service, ILogger<BlockMiddleware> logger)
        {
            _service = service;
            _logger = logger;
        }

        public bool Run(IApiClient sender, GenericEventArgs args)
        {
            switch (args)
            {
                case FriendMessageEventArgs friendMessage:
                {
                    var banned = _service.IsBanned(friendMessage.User.Identity, out var reason);
                    if (banned)
                        _logger.LogInformation("Message rejected ({FriendId}) for {Reason}",
                            friendMessage.User.Identity, reason);

                    return !banned;
                }
                case GroupMessageEventArgs groupMessage:
                {
                    var banned = _service.IsBanned(groupMessage.User.Identity, out var reason);
                    if (banned)
                        _logger.LogInformation("Message rejected: ({GroupId}) for {Reason}",
                            groupMessage.Group.Identity, reason);

                    return !banned;
                }
                default:
                    return true;
            }
        }
    }
}
