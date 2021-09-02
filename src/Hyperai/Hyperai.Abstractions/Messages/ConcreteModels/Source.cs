using System;

namespace Hyperai.Messages.ConcreteModels
{
    [Serializable]
    public class Source : MessageElement
    {
        public Source(long id)
        {
            MessageId = id;
        }

        public long MessageId { get; set; }

        public override int GetHashCode()
        {
            return MessageId.GetHashCode();
        }

        public override string ToString()
        {
            return $"<SOURCE {MessageId}>";
        }
    }
}
