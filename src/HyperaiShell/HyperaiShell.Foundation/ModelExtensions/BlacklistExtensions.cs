using Hyperai.Relations;
using HyperaiShell.Foundation.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HyperaiShell.Foundation.ModelExtensions
{
    public static class BlacklsitExtensions
    {
        private static readonly IBlockService service;

        static BlacklsitExtensions()
        {
            service = Shared.Host.Services.GetRequiredService<IBlockService>();
        }

        /// <summary>
        ///     是否被搬
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        public static bool IsBanned(this User user)
        {
            return service.IsBanned(user.Identity, out _);
        }

        /// <summary>
        ///     搬它
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="reason">理由</param>
        public static void Ban(this User user, string reason)
        {
            service.Ban(user.Identity, reason);
        }

        /// <summary>
        ///     反搬它
        /// </summary>
        /// <param name="user">用户</param>
        public static void Deban(this User user)
        {
            service.Deban(user.Identity);
        }
    }
}
