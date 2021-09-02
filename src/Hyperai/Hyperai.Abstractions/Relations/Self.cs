using System;
using System.Collections.Generic;

namespace Hyperai.Relations
{
    /// <summary>
    ///     表示当前登录的账号
    /// </summary>
    public sealed class Self : User
    {
        public override string Identifier => Identity.ToString();

        /// <summary>
        ///     自己所加过的群
        /// </summary>
        public Lazy<IEnumerable<Group>> Groups { get; set; }

        /// <summary>
        ///     自己所加过的好友
        /// </summary>
        public Lazy<IEnumerable<Friend>> Friends { get; set; }

        public override string ToString() => $"{Nickname ?? "NULL"}({Identifier ?? "UNKNOWN"})";
    }
}
