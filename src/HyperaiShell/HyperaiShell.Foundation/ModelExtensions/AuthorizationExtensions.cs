using System;
using System.Collections.Generic;
using System.Linq;
using Hyperai.Relations;
using HyperaiShell.Foundation.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HyperaiShell.Foundation.ModelExtensions
{
    public static class AuthorizationExtensions
    {
        private static readonly IAuthorizationService service;

        static AuthorizationExtensions()
        {
            service = Shared.Host.Services.GetRequiredService<IAuthorizationService>();
        }

        /// <summary>
        ///     授予限次权限
        /// </summary>
        /// <param name="model">宿主</param>
        /// <param name="name">权限名</param>
        /// <param name="count">次数</param>
        public static void GrantLimited(this RelationModel model, string name, int count)
        {
            service.PutLimited(model, name, count);
        }

        /// <summary>
        ///     授予限时权限
        /// </summary>
        /// <param name="model">宿主</param>
        /// <param name="name">权限名</param>
        /// <param name="expiration">期限</param>
        public static void GrantExpiry(this RelationModel model, string name, DateTime expiration)
        {
            service.PutExpiry(model, name, expiration);
        }

        /// <summary>
        ///     授予宿主特定权限
        /// </summary>
        /// <param name="model">宿主</param>
        /// <param name="name">权限名</param>
        public static void Grant(this RelationModel model, string name)
        {
            service.PutNormal(model, name);
        }

        /// <summary>
        ///     检查宿主是否具有某权限
        /// </summary>
        /// <param name="model">宿主</param>
        /// <param name="name">权限名</param>
        /// <returns></returns>
        public static bool CheckPermission(this RelationModel model, string name)
        {
            return service.CheckTicket(model, name);
        }

        /// <summary>
        ///     撤销权限
        /// </summary>
        /// <param name="model">宿主</param>
        /// <param name="name">权限名</param>
        public static void RevokePermission(this RelationModel model, string name)
        {
            service.RemoveTicket(model, name);
        }

        /// <summary>
        ///     获取全部由 Ticket 给予的权限
        /// </summary>
        /// <param name="model">宿主</param>
        /// <returns>权限名单</returns>
        public static IEnumerable<string> GetPermissions(this RelationModel model)
        {
            return service.GetTickets(model).Select(x => x.Name);
        }
    }
}
