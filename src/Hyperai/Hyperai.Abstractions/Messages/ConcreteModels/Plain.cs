using System;

namespace Hyperai.Messages.ConcreteModels
{
    [Serializable]
    public class Plain : MessageElement
    {
        /// <summary>
        ///     构造一个 <see cref="Plain" /> 对象
        /// </summary>
        /// <param name="text">表示的纯文本</param>
        /// <exception cref="ArgumentNullException" />
        public Plain(string text)
        {
            Text = text ?? throw new ArgumentNullException("Text cannot be null.");
        }

        public string Text { get; set; }

        public override int GetHashCode()
        {
            return Text.GetHashCode();
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
