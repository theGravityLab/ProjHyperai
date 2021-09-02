using System;
using System.Threading.Tasks;
using Hyperai.Events;
using Hyperai.Receipts;

namespace Hyperai.Services
{
    public interface IApiClient : IDisposable
    {
        ApiClientConnectionState State { get; }

        /// <summary>
        ///     监听事件发布, 阻塞线程
        /// </summary>
        void Listen();

        /// <summary>
        ///     连接
        /// </summary>
        void Connect();

        /// <summary>
        ///     断开连接不再接收和分发事件
        /// </summary>
        void Disconnect();

        /// <summary>
        ///     请求数据, 提供一个包含关键字段的模型, 返回同类型并包含请求数据的模型
        /// </summary>
        /// <typeparam name="T">模型类型</typeparam>
        /// <param name="model">包含关键字段的模型</param>
        /// <returns>全新模型实例</returns>
        Task<T> RequestAsync<T>(T model);

        /// <summary>
        ///     监听某一类型的事件
        /// </summary>
        /// <typeparam name="T">监听的具体事件类型</typeparam>
        /// <param name="handler">时间处理过程</param>
        void On<T>(IEventHandler<T> handler) where T : GenericEventArgs;

        /// <summary>
        ///     提交一个事件, 事件参数用 <typeparamref name="TArgs" /> 来表示
        /// </summary>
        /// <typeparam name="TArgs">事件类型</typeparam>
        /// <param name="args">包含具体参数的实例</param>
        /// <returns>回执</returns>
        Task<GenericReceipt> SendAsync<TArgs>(TArgs args) where TArgs : GenericEventArgs;
    }
}
