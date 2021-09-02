using Hyperai.Relations;

namespace Hyperai.Events
{
    /// <summary>
    /// 发: 撤回一条群内消息
    /// 收: 群消息撤回
    /// </summary>
    public sealed class GroupRecallEventArgs : RecallEventArgs
    {
        /// <summary>
        /// 消息的发送者
        /// </summary>
        public Member WhoseMessage { get; set; }
        /// <summary>
        /// 所在群
        /// </summary>
        public Group Group { get; set; }
        /// <summary>
        /// 操作者
        /// </summary>
        public Member Operator { get; set; }
    }
}
