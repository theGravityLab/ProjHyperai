namespace Hyperai.Messages
{
    public interface IMessageChainParser
    {
        MessageChain Parse(string text);
    }
}
