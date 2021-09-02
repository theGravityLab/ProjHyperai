using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hyperai.Messages
{
    /// <summary>
    ///     一份消息组件列表, 提供针对于消息组件的专有操作
    /// </summary>
    public sealed class MessageChain : IEnumerable<MessageElement>
    {
        /// <summary>
        ///     使用已经确定的 <see cref="MessageElement" /> 集合创建一个 <see cref="MessageChain" /> 实例
        /// </summary>
        /// <param name="list">组件集合</param>
        public MessageChain(IEnumerable<MessageElement> list)
        {
            Components = list ?? throw new ArgumentNullException(nameof(list), "List cannot be null.");
            Count = list.Count();
        }

        private MessageChain()
        {
        }

        public int Count { get; }
        internal IEnumerable<MessageElement> Components { get; set; }

        public IEnumerator<MessageElement> GetEnumerator()
        {
            return Components.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Components.GetEnumerator();
        }

        /// <summary>
        ///     获取消息的字符串表示, 特殊类型用的表示由 <see cref="MessageElement.ToString" /> 决定. 如果要序列化成支持反序列化的字符串请使用
        ///     <see cref="IMessageChainFormatter" />.
        /// </summary>
        /// <returns>对象的字符串表示</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var component in Components) sb.Append(component);
            return sb.ToString();
        }

        /// <summary>
        ///     判断是否与另一个消息内容完全相同
        /// </summary>
        /// <param name="other">对比的另一个实例</param>
        /// <returns></returns>
        public bool ChainEquals(MessageChain other)
        {
            if (other == null || other.Count != Count) return false;

            return Components.SequenceEqual(other.Components);
        }

        public static MessageChain Construct(params MessageElement[] components)
        {
            return new(components);
        }
    }
}
