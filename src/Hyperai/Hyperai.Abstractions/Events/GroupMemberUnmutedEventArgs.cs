using Hyperai.Relations;

namespace Hyperai.Events
{
    /// <summary>
    /// 发: 解除群成员的禁言
    /// 收: 群成员被管理员解除禁言
    /// </summary>
    public sealed class GroupMemberUnmutedEventArgs : GenericEventArgs
    {
        /// <summary>
        /// 操作者
        /// </summary>
        public Member Operator { get; set; }
        /// <summary>
        /// 所在群
        /// </summary>
        public Group Group { get; set; }
        /// <summary>
        /// 目标群成员
        /// </summary>
        public Member Whom { get; set; }
    }
}
