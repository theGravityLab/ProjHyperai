namespace Hyperai.Messages
{
    public abstract class MessageElement
    {
        public virtual string TypeName => GetType().Name;

        /// <summary>
        ///     将内容转换为字符串表示. 这将跳过无法表示的特殊类型, 或用符号代替.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Concat("<", TypeName.ToUpper(), ">");
        }

        /// <summary>
        ///     获取内容的哈希并用于比较相等性
        /// </summary>
        /// <returns></returns>
        public abstract override int GetHashCode();

        public override bool Equals(object obj)
        {
            return GetHashCode().Equals(obj?.GetHashCode());
        }

        public static bool operator ==(MessageElement a, MessageElement b)
        {
            return Equals(a, b);
        }

        public static bool operator !=(MessageElement a, MessageElement b)
        {
            return !(a == b);
        }
    }
}
