using Hyperai.Relations;

namespace Hyperai.Events
{
    /// <summary>
    /// 发: 修改群成员的权限
    /// 收: 群成员权限改变
    /// </summary>
    public sealed class GroupPermissionChangedEventArgs : GenericEventArgs
    {
        /// <summary>
        /// 目标成员
        /// </summary>
        public Member Whom { get; set; }
        /// <summary>
        /// 原权限
        /// </summary>
        public GroupRole Original { get; set; }
        /// <summary>
        /// 新权限
        /// </summary>
        public GroupRole Present { get; set; }
        /// <summary>
        /// 所在群
        /// </summary>
        public Group Group { get; set; }
    }
}
