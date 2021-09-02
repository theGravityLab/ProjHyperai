namespace Hyperai.Events
{
    /// <summary>
    /// 消息撤回事件
    /// </summary>
    public class RecallEventArgs : GenericEventArgs
    {
        /// <summary>
        /// 消息标志
        /// </summary>
        public long MessageId { get; set; }
    }
}
