namespace Hyperai.Events
{
    /// <summary>
    /// 发: 向目标发送好友请求
    /// 收: 收到目标的好友请求
    /// </summary>
    public sealed class FriendRequestEventArgs: GenericEventArgs
    {
        /// <summary>
        /// 发送请求的人
        /// </summary>
        public long Who { get; set; }
        /// <summary>
        /// 验证消息
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 好友请求标记
        /// </summary>
        public string Flag { get; set; }
    }
}
