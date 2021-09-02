using System;
using System.Linq;
using Hangfire.Dashboard;
using Hyperai.Services;
using HyperaiShell.Foundation.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HyperaiShell.App.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddClients(this IServiceCollection services, IConfiguration configuration)
        {
            var profileName = configuration["Application:SelectedProfile"];
            var profile = configuration
                .GetSection("Clients")
                .GetChildren()
                .First(x => x["Name"] == profileName);
            var clientType = Type.GetType(profile["ClientTypeDefined"], true);
            var optionsType = Type.GetType(profile["OptionsTypeDefined"], true);
            var optionsSection = profile.GetSection("Options");
            services.AddSingleton(typeof(IApiClient), clientType!);
            services.AddSingleton(optionsType, optionsSection.Get(optionsType));

            return services;
        }

        public static IServiceCollection AddBots(this IServiceCollection services)
        {
            services.AddSingleton<IBotService, BotService>();
            return services;
        }

        public static IServiceCollection AddAuthorizationService(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationService, AuthorizationService>();
            return services;
        }

        public static IServiceCollection AddAttachments(this IServiceCollection services)
        {
            services.AddSingleton<IAttachmentService, AttachmentService>();
            return services;
        }

        public static IServiceCollection AddBlacklist(this IServiceCollection services)
        {
            services.AddSingleton<IBlockService, BlockService>();
            return services;
        }
    }
}
