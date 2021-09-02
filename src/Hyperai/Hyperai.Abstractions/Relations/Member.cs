using System;

namespace Hyperai.Relations
{
    public enum GroupRole
    {
        Owner,
        Member,
        Administrator
    }

    /// <summary>
    ///     群中的成员,必须位于一个群内
    /// </summary>
    public sealed class Member : User
    {
        public override string Identifier => $"{Identity}@{Group.Value.Identity}";

        /// <summary>
        ///     头衔
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     群名片
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///     是否被禁言
        /// </summary>
        public bool IsMuted { get; set; }

        /// <summary>
        ///     所在群
        /// </summary>
        public Lazy<Group> Group { get; set; }

        /// <summary>
        ///     所在群中的角色
        /// </summary>
        public GroupRole Role { get; set; }

        public override string ToString() => $"{DisplayName ?? "NULL"}({Identifier ?? "UNKNOWN"})";
    }
}
