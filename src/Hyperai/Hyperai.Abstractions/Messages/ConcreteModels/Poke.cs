using System;

namespace Hyperai.Messages.ConcreteModels
{
    public enum PokeType
    {
        /// <summary>
        ///     戳一戳
        /// </summary>
        Poke = 1,

        /// <summary>
        ///     比心
        /// </summary>
        ShowLove,

        /// <summary>
        ///     点赞
        /// </summary>
        Like,

        /// <summary>
        ///     心碎
        /// </summary>
        Heartbroken,

        /// <summary>
        ///     666
        /// </summary>
        SixSixSix,

        /// <summary>
        ///     放大招
        /// </summary>
        FangDaZhao
    }

    [Serializable]
    public class Poke : MessageElement
    {
        public Poke(PokeType name)
        {
            Name = name;
        }

        public PokeType Name { get; private set; }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return $"<POKE {Name}>";
        }
    }
}
