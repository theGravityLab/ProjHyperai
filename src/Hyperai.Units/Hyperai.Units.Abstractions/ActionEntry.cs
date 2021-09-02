using System;
using System.Reflection;
using Hyperai.Events;

namespace Hyperai.Units
{
    public struct ActionEntry
    {
        public MessageEventType Type { get; }
        public MethodInfo Action { get; }
        public Type Unit { get; }
        public object State { get; set; }

        public ActionEntry(MessageEventType type, MethodInfo action, Type unit, object state)
        {
            Type = type;
            Action = action;
            Unit = unit;
            State = state;
        }

        public override string ToString()
        {
            return $"{Unit.Name}.{Action.Name}@{Type}";
        }
    }
}
