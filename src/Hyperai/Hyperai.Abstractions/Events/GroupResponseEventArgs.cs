namespace Hyperai.Events
{
    /// <summary>
    ///     发: 响应入群申请
    /// </summary>
    public sealed class GroupResponseEventArgs : GenericEventArgs
    {
        public enum ResponseOperation
        {
            Approve,
            Reject
        }

        /// <summary>
        ///     请求标志
        /// </summary>
        public string Flag { get; set; }

        /// <summary>
        ///     操作
        /// </summary>
        public ResponseOperation Operation { get; set; }

        /// <summary>
        ///     拒绝理由
        /// </summary>
        public string Reason { get; set; }
    }
}
