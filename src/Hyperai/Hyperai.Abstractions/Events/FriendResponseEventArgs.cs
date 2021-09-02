namespace Hyperai.Events
{
    /// <summary>
    /// 发: 响应好友请求
    /// 收: 好友请求被响应
    /// </summary>
    public sealed class FriendResponseEventArgs: GenericEventArgs
    {
        /// <summary>
        /// 被通过申请的人
        /// </summary>
        public long Who { get; set; }
        /// <summary>
        /// 好友请求标记
        /// </summary>
        public string Flag { get; set; }
        /// <summary>
        /// 好友请求的结果
        /// </summary>
        public ResponseOperation Operation { get; set; }
        
        public enum ResponseOperation
        {
            Ignore,
            Approve,
            Reject
        }
    }
}
