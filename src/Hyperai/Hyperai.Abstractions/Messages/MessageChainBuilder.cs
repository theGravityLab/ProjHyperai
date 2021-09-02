using System.Collections.Generic;
using IBuilder;

namespace Hyperai.Messages
{
    public sealed class MessageChainBuilder : IBuilder<MessageChain>
    {
        private readonly List<MessageElement> components = new();

        public MessageChain Build()
        {
            var chain = new MessageChain(components);
            return chain;
        }

        public MessageChainBuilder Add(MessageElement element)
        {
            components.Add(element);
            return this;
        }

        public MessageChainBuilder AddRange(IEnumerable<MessageElement> elements)
        {
            components.AddRange(elements);
            return this;
        }
    }
}
