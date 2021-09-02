namespace Hyperai.Relations
{
    public abstract class RelationModel
    {
        /// <summary> 表示事实上的一个对象,不同实例可以有同一个 <see cref="Identity" />. </summary>
        public long Identity { get; set; }

        /// <summary> 区分模型与模型, 同一个用户在不同群则 <see cref="Identifier" /> 具有不同的值 </summary>
        public abstract string Identifier { get; }

        public override int GetHashCode()
        {
            return Identifier.GetHashCode();
        }
    }
}
