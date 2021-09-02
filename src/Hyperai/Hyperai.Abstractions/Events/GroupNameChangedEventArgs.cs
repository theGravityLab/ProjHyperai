using Hyperai.Relations;

namespace Hyperai.Events
{
    /// <summary>
    /// 发: 修改群名字
    /// 收: 群名字改变
    /// </summary>
    public sealed class GroupNameChangedEventArgs : GenericEventArgs
    {
        /// <summary>
        /// 原名字
        /// </summary>
        public string Original { get; set; }
        /// <summary>
        /// 新名字
        /// </summary>
        public string Present { get; set; }
        /// <summary>
        /// 所在群
        /// </summary>
        public Group Group { get; set; }
        /// <summary>
        /// 修改者
        /// </summary>
        public Member Operator { get; set; }
    }
}
