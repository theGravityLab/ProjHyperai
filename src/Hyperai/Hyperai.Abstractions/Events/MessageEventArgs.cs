using Hyperai.Messages;

namespace Hyperai.Events
{
    /// <summary>
    /// 消息发送事件
    /// </summary>
    public abstract class MessageEventArgs : GenericEventArgs
    {
        /// <summary>
        /// 消息
        /// </summary>
        public MessageChain Message { get; set; }
    }
}
