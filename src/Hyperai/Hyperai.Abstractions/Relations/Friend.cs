namespace Hyperai.Relations
{
    /// <summary>
    ///     表示位于好友列表中的用户
    /// </summary>
    public class Friend : User
    {
        public override string Identifier => Identity.ToString();

        /// <summary>
        ///     备注
        /// </summary>
        public string Remark { get; set; }

        public override string ToString() => $"{Remark ?? "NULL"}({Identifier ?? "UNKNOWN"})";
    }
}
