using System;
using System.Collections.Generic;
using Hyperai.Messages.ConcreteModels;
using Hyperai.Relations;

namespace Hyperai.Units
{
    public interface IUnitService
    {
        /// <summary>
        ///     搜索整个程序集查找并添加 <see cref="UnitBase" /> 到可用列表
        /// </summary>
        void SearchForUnits();

        IEnumerable<ActionEntry> GetEntries();

        /// <summary>
        ///     处理并分发一个消息上下文, 线程不安全
        /// </summary>
        /// <param name="context">目标上下文, 其中的 <see cref="MessageContext.Message" /> 不应该包含不可序列化元素和 <see cref="Source" /> 元素</param>
        void Handle(MessageContext context);

        /// <summary>
        ///     将一个通道加入到独占队列, 到达该通道的第一条消息将被目标委托所捕获. 这个过程是一次性的
        /// </summary>
        /// <param name="channel">所要占据的通道</param>
        /// <param name="action"> 目标委托</param>
        /// <param name="timeout">自加入起的一定时间后无效化</param>
        void WaitOne(Signature channel, ActionDelegate action, TimeSpan timeout);
    }
}
