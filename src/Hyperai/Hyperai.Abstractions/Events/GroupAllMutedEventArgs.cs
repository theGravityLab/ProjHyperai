using Hyperai.Relations;

namespace Hyperai.Events
{
    /// <summary>
    /// 发: 开启或关闭禁言
    /// 收: 群被管理员开启或关闭禁言
    /// </summary>
    public sealed class GroupAllMutedEventArgs : GenericEventArgs
    {
        /// <summary>
        /// 开启或关闭禁言的管理员
        /// </summary>
        public Member Operator { get; set; }
        /// <summary>
        /// 目标群
        /// </summary>
        public Group Group { get; set; }
        /// <summary>
        /// 开启或是关闭
        /// </summary>
        public bool IsEnded { get; set; }
    }
}
