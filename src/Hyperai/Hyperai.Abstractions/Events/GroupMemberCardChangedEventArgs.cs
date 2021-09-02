using Hyperai.Relations;

namespace Hyperai.Events
{
    /// <summary>
    /// 发: 修改群成员的群名片
    /// 收： 群成员的名片被自己或管理员修改
    /// </summary>
    public sealed class GroupMemberCardChangedEventArgs : GenericEventArgs
    {
        /// <summary>
        /// 原名片
        /// </summary>
        public string Original { get; set; }
        /// <summary>
        /// 新名片
        /// </summary>
        public string Present { get; set; }
        /// <summary>
        /// 自己或管理员
        /// </summary>
        public Member Operator { get; set; }
        /// <summary>
        /// 目标成员
        /// </summary>
        public Member WhoseName { get; set; }
        /// <summary>
        /// 所在群
        /// </summary>
        public Group Group { get; set; }
    }
}
