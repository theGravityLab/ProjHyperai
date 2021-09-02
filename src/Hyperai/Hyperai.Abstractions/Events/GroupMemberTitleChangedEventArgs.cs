using Hyperai.Relations;

namespace Hyperai.Events
{
    /// <summary>
    /// 发: 修改群成员头衔
    /// 收: 群成员获得群主授予的头衔
    /// </summary>
    public sealed class GroupMemberTitleChangedEventArgs : GenericEventArgs
    {
        /// <summary>
        /// 原头衔
        /// </summary>
        public string Original { get; set; }
        /// <summary>
        /// 新头衔
        /// </summary>
        public string Present { get; set; }
        /// <summary>
        /// 所在群
        /// </summary>
        public Group Group { get; set; }
        /// <summary>
        /// 目标群成员
        /// </summary>
        public Member Who { get; set; }
    }
}
