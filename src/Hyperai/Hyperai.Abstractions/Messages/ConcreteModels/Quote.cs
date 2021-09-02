using System;

namespace Hyperai.Messages.ConcreteModels
{
    [Serializable]
    public class Quote : MessageElement
    {
        public Quote(long messageId)
        {
            MessageId = messageId;
        }

        public long MessageId { get; set; }

        public override int GetHashCode()
        {
            return MessageId.GetHashCode();
        }

        public override string ToString()
        {
            return $"<QUOTE {MessageId}>";
        }
    }
}
