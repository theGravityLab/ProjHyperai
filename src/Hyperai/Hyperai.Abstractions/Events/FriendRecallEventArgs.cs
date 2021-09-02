using Hyperai.Relations;

namespace Hyperai.Events
{
    /// <summary>
    /// 收: 好友消息撤回
    /// </summary>
    public sealed class FriendRecallEventArgs : RecallEventArgs
    {
        /// <summary>
        /// 目标好友
        /// </summary>
        public Friend WhoseMessage { get; set; }
    }
}
