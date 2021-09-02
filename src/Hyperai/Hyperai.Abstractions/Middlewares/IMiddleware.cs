using Hyperai.Events;
using Hyperai.Services;

namespace Hyperai.Middlewares
{
    public interface IMiddleware
    {
        /// <summary>
        ///     执行中间件功能
        /// </summary>
        /// <param name="sender">取得事件的 <see cref="IApiClient" /> 头部</param>
        /// <param name="args">待处理的事件参数</param>
        /// <returns>是否阻断事件处理管线, false 意味着否</returns>
        bool Run(IApiClient sender, GenericEventArgs args);
    }
}
