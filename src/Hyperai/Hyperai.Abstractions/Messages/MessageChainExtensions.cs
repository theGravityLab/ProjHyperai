using System.Linq;
using Hyperai.Messages.ConcreteModels;

namespace Hyperai.Messages
{
    public static class MessageChainExtensions
    {
        /// <summary>
        ///     是否能被作为引用对象, 用于区分手工搓的消息链和远端接收到的消息链
        /// </summary>
        /// <param name="chain">判断哪个消息链</param>
        /// <returns>能否被引用</returns>
        public static bool CanBeReplied(this MessageChain chain)
        {
            return chain.Any(x => x is Source);
        }

        /// <summary>
        ///     当能被引用时则产生一个包含引用信息的消息构造器
        /// </summary>
        /// <param name="chain">被引用的消息链</param>
        /// <returns>包含引用信息的消息构造器</returns>
        public static MessageChainBuilder MakeReply(this MessageChain chain)
        {
            var builder = new MessageChainBuilder();
            builder.AddQuote(((Source) chain.First(x => x is Source)).MessageId);
            return builder;
        }

        /// <summary>
        ///     返回当前消息链的可读形式, 即去除了 <see cref="Source" /> 元素
        /// </summary>
        /// <param name="chain">原链</param>
        /// <returns>不包含不便于程序阅读元素的新链</returns>
        public static MessageChain AsReadable(this MessageChain chain)
        {
            return new(chain.Where(x => !(x is Source)));
        }

        /// <summary>
        ///     返回当前消息链的适用于发送形式, 去除仅用于接收的消息元素, 即去除了 <see cref="Source" /> 和 <see cref="Quote" />
        /// </summary>
        /// <param name="chain">原链</param>
        /// <returns>包含不便发送元素的新链</returns>
        public static MessageChain AsSendable(this MessageChain chain)
        {
            return new(chain.Where(x => !(x is Source) && !(x is Quote)));
        }
    }
}
