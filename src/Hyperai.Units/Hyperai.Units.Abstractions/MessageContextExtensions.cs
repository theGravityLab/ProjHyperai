using System.Threading.Tasks;
using Hyperai.Events;
using Hyperai.Messages;
using Hyperai.Relations;
using Hyperai.Services;

namespace Hyperai.Units
{
    public static class MessageContextExtensions
    {
        public static async Task ReplyAsync(this MessageContext context, MessageChain message)
        {
            switch (context.Type)
            {
                case MessageEventType.Friend:
                    await context.Client.SendFriendMessageAsync((Friend) context.User, message);
                    break;

                case MessageEventType.Group:
                    await context.Client.SendGroupMessageAsync(context.Group, message);
                    break;
            }
        }
    }
}
