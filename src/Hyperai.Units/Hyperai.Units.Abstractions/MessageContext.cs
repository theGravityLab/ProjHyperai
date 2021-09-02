using System;
using Hyperai.Events;
using Hyperai.Messages;
using Hyperai.Relations;
using Hyperai.Services;

namespace Hyperai.Units
{
    public sealed class MessageContext
    {
        public User User { get; set; }
        public Group Group { get; set; }
        public MessageEventType Type { get; set; }
        public MessageChain Message { get; set; }
        public DateTime SentAt { get; set; }
        public IApiClient Client { get; set; }
        public Self Me { get; set; }
    }
}
