using System;
using Hyperai.Events;

namespace Hyperai.Units.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ReceiveAttribute : Attribute
    {
        public ReceiveAttribute(MessageEventType type)
        {
            Type = type;
        }

        public ReceiveAttribute() : this(MessageEventType.Friend)
        {
        }

        public MessageEventType Type { get; set; }
    }
}
