using System.Threading.Tasks;
using Hyperai.Messages;
using Hyperai.Messages.ConcreteModels;
using Hyperai.Relations;
using Hyperai.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HyperaiShell.Foundation.ModelExtensions
{
    public static class ClientExtensions
    {
        private static readonly IApiClient _client;
        private static readonly ILogger _logger;

        static ClientExtensions()
        {
            _client = Shared.Host.Services.GetRequiredService<IApiClient>();
            _logger = Shared.Host.Services.GetRequiredService<ILoggerFactory>()
                .CreateLogger(typeof(ClientExtensions).AssemblyQualifiedName);
        }

        /// <summary>
        ///     使用默认 <see cref="IApiClient" /> 发送 <see cref="MessageChain" />
        /// </summary>
        /// <param name="friend">好友</param>
        /// <param name="message">消息链</param>
        public static async Task SendAsync(this Friend friend, MessageChain message)
        {
            _logger.LogInformation("{Client}({Type}) < {Friend}:\n{Message}", _client.GetType().Name, nameof(Friend),
                friend.Identifier,
                message);
            await _client.SendFriendMessageAsync(friend, message);
        }

        /// <summary>
        ///     使用默认 <see cref="IApiClient" /> 发送 <see cref="string" /> 构成的 <see cref="MessageChain" />
        /// </summary>
        /// <param name="friend">好友</param>
        /// <param name="plain">消息串</param>
        public static async Task SendPlainAsync(this Friend friend, string plain)
        {
            _logger.LogInformation("{Client}({Type}) < {Friend}:\n{Message}", _client.GetType().Name, nameof(Friend),
                friend.Identifier,
                plain);
            await _client.SendFriendMessageAsync(friend, MessageChain.Construct(new Plain(plain)));
        }

        /// <summary>
        ///     使用默认 <see cref="IApiClient" /> 发送 <see cref="MessageChain" />
        /// </summary>
        /// <param name="group">群</param>
        /// <param name="message">消息链</param>
        public static async Task SendAsync(this Group group, MessageChain message)
        {
            _logger.LogInformation("{Client}({Type}) < {Group}:\n{Message}", _client.GetType().Name, nameof(Group),
                group.Identifier,
                message);
            await _client.SendGroupMessageAsync(group, message);
        }

        /// <summary>
        ///     使用默认 <see cref="IApiClient" /> 发送 <see cref="string" /> 构成的 <see cref="MessageChain" />
        /// </summary>
        /// <param name="group">群</param>
        /// <param name="plain">消息串</param>
        public static async Task SendPlainAsync(this Group group, string plain)
        {
            _logger.LogInformation("{Client}({Type}) < {Group}:\n{Message}", _client.GetType().Name, nameof(Group),
                group.Identifier,
                plain);
            await _client.SendGroupMessageAsync(group, MessageChain.Construct(new Plain(plain)));
        }
    }
}
