namespace Hyperai.Relations
{
    public abstract class User : RelationModel
    {
        /// <summary>
        ///     用户昵称
        /// </summary>
        public string Nickname { get; set; }
    }
}
