using System;
using System.Collections.Generic;

namespace Hyperai.Relations
{
    /// <summary>
    ///     表示一个群,通常是自己所加过的
    /// </summary>
    public sealed class Group : RelationModel
    {
        public override string Identifier => Identity.ToString();

        /// <summary>
        ///     群名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     群成员列表(会构成循环引用)
        /// </summary>
        public Lazy<IEnumerable<Member>> Members { get; set; }

        /// <summary>
        ///     群主
        /// </summary>
        public Lazy<Member> Owner { get; set; }

        public override string ToString() => $"{Name ?? "NULL"}({Identifier ?? "UNKNOWN"})";
    }
}
