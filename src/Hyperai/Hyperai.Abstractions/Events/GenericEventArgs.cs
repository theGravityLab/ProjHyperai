using System;

namespace Hyperai.Events
{
    /// <summary>
    /// 一般事件的所有基类
    /// </summary>
    public class GenericEventArgs
    {
        /// <summary>
        /// 发生该事件的事件
        /// </summary>
        public DateTime Time { get; set; } = DateTime.Now;
    }
}
