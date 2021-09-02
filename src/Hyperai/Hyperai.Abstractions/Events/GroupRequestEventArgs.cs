namespace Hyperai.Events
{
    /// <summary>
    /// 发: 申请加入群
    /// 收: 收到入群的申请
    /// </summary>
    public sealed class GroupRequestEventArgs: GenericEventArgs
    {
        /// <summary>
        /// 群号
        /// </summary>
        public long GroupId { get; set; }
        /// <summary>
        /// 发起人
        /// </summary>
        public long Who { get; set; }
        /// <summary>
        /// 验证消息
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 请求标志
        /// </summary>
        public string Flag { get; set; }
    }
}
