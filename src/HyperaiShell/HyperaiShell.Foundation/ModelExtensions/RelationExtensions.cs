using System;
using Hyperai.Relations;
using Hyperai.Services;
using Hyperai.Units;
using Microsoft.Extensions.DependencyInjection;

namespace HyperaiShell.Foundation.ModelExtensions
{
    public static class RelationExtensions
    {
        private static readonly IApiClient client;
        private static readonly IUnitService unit;

        static RelationExtensions()
        {
            client = Shared.Host.Services.GetRequiredService<IApiClient>();
            unit = Shared.Host.Services.GetRequiredService<IUnitService>();
        }

        /// <summary>
        ///     在确定群内有该成员的前提下用其 id 获取成员其他信息
        /// </summary>
        /// <param name="group">目标群</param>
        /// <param name="identity">TA滴 id</param>
        /// <returns>完整的群员信息</returns>
        public static Member GetMember(this Group group, long identity)
        {
            return client.RequestAsync(new Member {Identity = identity, Group = new Lazy<Group>(group)}).GetAwaiter()
                .GetResult();
        }


        /// <summary>
        ///     监听该 <see cref="Friend" /> 的下一条消息
        /// </summary>
        /// <param name="friend">目标好友</param>
        /// <param name="action">当消息抵达时的操作</param>
        /// <param name="msToExpire">过期时间(ms)</param>
        public static void Await(this Friend friend, ActionDelegate action, int msToExpire = 30000)
        {
            unit.WaitOne(Signature.FromFriend(friend.Identity), action, TimeSpan.FromMilliseconds(msToExpire));
        }

        /// <summary>
        ///     监听该 <see cref="Group" /> 的下一条消息
        /// </summary>
        /// <param name="group">目标群</param>
        /// <param name="action">当消息抵达时的操作</param>
        /// <param name="msToExpire">过期时间(ms)</param>
        public static void Await(this Group group, ActionDelegate action, int msToExpire = 30000)
        {
            unit.WaitOne(Signature.FromGroup(group.Identity), action, TimeSpan.FromMilliseconds(msToExpire));
        }

        /// <summary>
        ///     监听该 <see cref="Member" /> 的下一条消息
        /// </summary>
        /// <param name="member">目标成员</param>
        /// <param name="action">当消息抵达时的操作</param>
        /// <param name="msToExpire">过期时间(ms)</param>
        public static void Await(this Member member, ActionDelegate action, int msToExpire = 30000)
        {
            unit.WaitOne(Signature.FromMember(member.Group.Value.Identity, member.Identity), action,
                TimeSpan.FromMilliseconds(msToExpire));
        }
    }
}
