using System;

namespace Hyperai.Messages.ConcreteModels
{
    [Serializable]
    public class At : MessageElement
    {
        public At(long targetId)
        {
            TargetId = targetId;
        }

        public long TargetId { get; set; }

        public override int GetHashCode()
        {
            return TargetId.GetHashCode();
        }

        public override string ToString()
        {
            return $"<AT {TargetId}>";
        }
    }
}
