using Hyperai.Relations;

namespace Hyperai.Events
{
    /// <summary>
    /// 收: 新成员加入
    /// </summary>
    public sealed class GroupJoinedEventArgs : GenericEventArgs
    {
        /// <summary>
        /// 新成员
        /// </summary>
        public Member Who { get; set; }
        /// <summary>
        /// 目标群
        /// </summary>
        public Group Group { get; set; }
        /// <summary>
        /// 操作管理员
        /// </summary>
        public Member Operator { get; set; }
    }
}
