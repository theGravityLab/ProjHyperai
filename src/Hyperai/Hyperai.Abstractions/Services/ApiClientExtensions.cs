using System;
using System.Threading.Tasks;
using Hyperai.Events;
using Hyperai.Messages;
using Hyperai.Receipts;
using Hyperai.Relations;

namespace Hyperai.Services
{
    public static class ApiClientExtensions
    {
        public static IApiClient On<T>(this IApiClient client, Action<IApiClient, T> action) where T : GenericEventArgs
        {
            client.On(new DefaultEventHandler<T>(client, action));
            return client;
        }

        public static async Task<MessageReceipt> SendFriendMessageAsync(this IApiClient client, Friend friend,
            MessageChain message)
        {
            var args = new FriendMessageEventArgs
            {
                Message = message,
                User = friend
            };
            return (MessageReceipt) await client.SendAsync(args);
        }

        public static async Task<MessageReceipt> SendGroupMessageAsync(this IApiClient client, Group group,
            MessageChain message)
        {
            var args = new GroupMessageEventArgs
            {
                Message = message,
                Group = group
            };
            return (MessageReceipt) await client.SendAsync(args);
        }

        public static async Task RevokeMessageAsync(this IApiClient client, long messageId)
        {
            var args = new RecallEventArgs
            {
                MessageId = messageId
            };
            await client.SendAsync(args);
        }

        public static async Task KickAsync(this IApiClient client, Group group, Member member)
        {
            var args = new GroupLeftEventArgs
            {
                Group = group,
                IsKicked = true,
                Who = member
            };
            await client.SendAsync(args);
        }

        public static async Task QuitAsync(this IApiClient client, Group group)
        {
            var me = await client.RequestAsync(new Self());
            var args = new GroupLeftEventArgs
            {
                Group = group,
                IsKicked = false,
                Who = new Member()
                {
                    Group = new Lazy<Group>(group),
                    Identity = me.Identity
                    
                }
            };
            await client.SendAsync(args);
        }

        public static async Task MuteAsync(this IApiClient client, Group group, Member member, TimeSpan duration)
        {
            var args = new GroupMemberMutedEventArgs
            {
                Group = group,
                Whom = member,
                Duration = duration
            };
            await client.SendAsync(args);
        }

        public static async Task MuteAllAsync(this IApiClient client, Group group)
        {
            var args = new GroupAllMutedEventArgs
            {
                Group = group,
                IsEnded = false
            };
            await client.SendAsync(args);
        }
    }
}
