namespace Hyperai.Messages.ConcreteModels
{
    public class Node: MessageElement
    {
        public Node(long userId, string userDisplayName, MessageChain message)
        {
            UserId = userId;
            UserDisplayName = userDisplayName;
            Message = message;
        }

        public long UserId { get; set; }
        public string UserDisplayName { get; set; }
        public MessageChain Message { get; set; }

        public override int GetHashCode() => 0; // 没法比较默认判相等
    }
}
