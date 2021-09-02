using System;
using Hyperai.Relations;

namespace Hyperai.Events
{
    /// <summary>
    /// 发: 禁言某个群成员
    /// 收: 群成员被管理员禁言
    /// </summary>
    public sealed class GroupMemberMutedEventArgs : GenericEventArgs
    {
        /// <summary>
        /// 操作的管理员
        /// </summary>
        public Member Operator { get; set; }
        /// <summary>
        /// 所在群
        /// </summary>
        public Group Group { get; set; }
        /// <summary>
        /// 禁言时常
        /// </summary>
        public TimeSpan Duration { get; set; }
        /// <summary>
        /// 目标群员
        /// </summary>
        public Member Whom { get; set; }
    }
}
