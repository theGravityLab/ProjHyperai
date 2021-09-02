using System;
using Hyperai.Relations;
using HyperaiShell.Foundation.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HyperaiShell.Foundation.ModelExtensions
{
    public static class AttachmentExtensions
    {
        private static readonly IAttachmentService service;

        static AttachmentExtensions()
        {
            service = Shared.Host.Services.GetRequiredService<IAttachmentService>();
        }

        /// <summary>
        ///     将一个对象附加到 <see cref="RelationModel" /> 上, 有则替换
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="model">目标关系模型</param>
        /// <param name="ins">实例</param>
        public static void Attach<T>(this RelationModel model, T ins)
        {
            service.Attach(ins, model);
        }

        /// <summary>
        ///     将 <typeparamref name="T" /> 类型的对象从 <see cref="RelationModel" /> 上移除
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="model">目标关系模型</param>
        public static void Detach<T>(this RelationModel model)
        {
            service.Detach<T>(model);
        }

        /// <summary>
        ///     获取附加在 <see cref="RelationModel" /> 上的 <typeparamref name="T" /> 对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="model">目标关系模型</param>
        /// <param name="generator">当对象不存在时返回该生成器创建的对象（不会附加</param>
        /// <returns></returns>
        public static T Retrieve<T>(this RelationModel model, Func<T> generator = null)
        {
            generator ??= () => default;
            var t = service.Retrieve<T>(model);
            if (t != null) return t;

            t = generator();
            if (t == null) return default;

            service.Attach(t, model);
            return t;
        }

        /// <summary>
        ///     获取 <typeparamref name="T" /> 类型对象并使用 using 语句来自动提交更新
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="model">目标关系模型</param>
        /// <param name="ins">得到的实例</param>
        /// <param name="generator">当对象不存在时返回该生成器创建的对象（会附加</param>
        /// <returns></returns>
        public static ForAttachmentUpdateScope<T> For<T>(this RelationModel model, out T ins, Func<T> generator = null)
        {
            return service.For(model, out ins, generator);
        }
    }
}
