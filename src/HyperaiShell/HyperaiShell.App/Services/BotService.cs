using System;
using System.Threading;
using System.Threading.Tasks;
using Hyperai.Events;
using Hyperai.Relations;
using Hyperai.Services;
using HyperaiShell.App.Bots;
using HyperaiShell.Foundation.Bots;
using HyperaiShell.Foundation.Services;
using Microsoft.Extensions.Logging;
using Sentry;

namespace HyperaiShell.App.Services
{
    public class BotService : IBotService
    {
        private readonly IApiClient _client;
        private readonly ILogger _logger;
        private readonly IServiceProvider _provider;
        private readonly IHub _hub;
        private BotCollection bots;

        public BotService(IApiClient client, IServiceProvider provider, ILogger<BotService> logger, IHub hub)
        {
            _client = client;
            _provider = provider;
            _logger = logger;
            _hub = hub;
        }

        public IBotCollectionBuilder Builder { get; } = new BotCollectionBuilder();

        public async Task PushAsync(GenericEventArgs args)
        {
            var transaction = _hub.StartTransaction($"{nameof(HyperaiShell)}-{nameof(BotService)}", nameof(PushAsync),
                args.GetType().Name);
            var self = await _client.RequestAsync<Self>(null);

            switch (args)
            {
                case FriendMessageEventArgs it:
                    await DoForAllAsync(x => x.OnFriendMessage(_client, it), self);
                    break;
                case GroupMessageEventArgs it:
                    await DoForAllAsync(x => x.OnGroupMessage(_client, it), self);
                    break;
                case FriendRecallEventArgs it:
                    await DoForAllAsync(x => x.OnFriendRecall(_client, it), self);
                    break;
                case GroupRecallEventArgs it:
                    await DoForAllAsync(x => x.OnGroupRecall(_client, it), self);
                    break;
                case GroupMemberMutedEventArgs it:
                    await DoForAllAsync(x => x.OnGroupMemberMuted(_client, it), self);
                    break;
                case GroupMemberUnmutedEventArgs it:
                    await DoForAllAsync(x => x.OnGroupMemberUnmuted(_client, it), self);
                    break;
                case GroupAllMutedEventArgs it:
                    await DoForAllAsync(x => x.OnGroupAllMuted(_client, it), self);
                    break;
                case GroupLeftEventArgs it:
                    await DoForAllAsync(x => x.OnGroupLeft(_client, it), self);
                    break;
                case GroupJoinedEventArgs it:
                    await DoForAllAsync(x => x.OnGroupJoined(_client, it), self);
                    break;
                case GroupMemberCardChangedEventArgs it:
                    await DoForAllAsync(x => x.OnGroupMemberCardChanged(_client, it), self);
                    break;
                case GroupMemberTitleChangedEventArgs it:
                    await DoForAllAsync(x => x.OnGroupMemberTitleChanged(_client, it), self);
                    break;
                case GroupPermissionChangedEventArgs it:
                    await DoForAllAsync(x => x.OnGroupPermissionChanged(_client, it), self);
                    break;
                case FriendRequestEventArgs it:
                    await DoForAllAsync(x => x.OnFriendRequest(_client, it), self);
                    break;
                case GroupRequestEventArgs it:
                    await DoForAllAsync(x => x.OnGroupRequest(_client, it), self);
                    break;
            }
            await DoForAllAsync(x => x.OnEverything(_client, args), self);
            transaction.Finish();
        }

        private async Task DoForAllAsync(Action<BotBase> action, Self me)
        {
            bots ??= Builder.Build(_provider);

            foreach (var bot in bots)
            {
                bot.Me = me;
                var task = Task.Run(() => action(bot), CancellationToken.None);
                await task;
                if (task.IsFaulted)
                    _logger.LogError(task.Exception, "Bot({BotType}) action({ActionName}) exited unsuccessfully",
                        bot.GetType().Name, action.Method.Name);
            }
        }
    }
}
