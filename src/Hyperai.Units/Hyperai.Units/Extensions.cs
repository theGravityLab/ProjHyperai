using Microsoft.Extensions.DependencyInjection;

namespace Hyperai.Units
{
    public static class Extensions
    {
        /// <summary>
        ///     添加 Units 服务但不会搜索程序集里的 <see cref="UnitBase" />
        /// </summary>
        /// <param name="services">service collection</param>
        /// <returns>original service collection</returns>
        public static IServiceCollection AddUnits(this IServiceCollection services)
        {
            services.AddSingleton<IUnitService, UnitService>();
            return services;
        }

        public static HyperaiServerOptionsBuilder UseUnits(this HyperaiServerOptionsBuilder builder)
        {
            builder.Use<UnitMiddleware>();
            return builder;
        }
    }
}
