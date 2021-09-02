using Hyperai.Relations;

namespace Hyperai.Events
{
    /// <summary>
    /// 发: 发送群消息
    /// 收: 收到群消息
    /// </summary>
    public sealed class GroupMessageEventArgs : MessageEventArgs
    {
        /// <summary>
        /// 发送者
        /// </summary>
        public Member User { get; set; }
        /// <summary>
        /// 所在群
        /// </summary>
        public Group Group { get; set; }
    }
}
