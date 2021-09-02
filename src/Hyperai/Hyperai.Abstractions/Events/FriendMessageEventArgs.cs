using Hyperai.Relations;

namespace Hyperai.Events
{
    /// <summary>
    /// 发: 向好友发送消息
    /// 收: 收到好友消息
    /// </summary>
    public sealed class FriendMessageEventArgs : MessageEventArgs
    {
        /// <summary>
        /// 消息目标
        /// </summary>
        public Friend User { get; set; }
    }
}
