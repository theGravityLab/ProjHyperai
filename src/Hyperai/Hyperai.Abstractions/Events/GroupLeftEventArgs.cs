using Hyperai.Relations;

namespace Hyperai.Events
{
    /// <summary>
    /// 发: 踢出成员
    /// 收: 有成员退出
    /// </summary>
    public sealed class GroupLeftEventArgs : GenericEventArgs
    {
        
        public Member Who { get; set; }
        public bool IsKicked { get; set; }
        public Member Operator { get; set; }

        public Group Group { get; set; }
    }
}
