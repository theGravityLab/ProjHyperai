using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Hyperai.Services;
using Hyperai.Units;
using HyperaiShell.App.Data;
using HyperaiShell.App.Logging.ConsoleFormatters;
using HyperaiShell.App.Packages;
using HyperaiShell.App.Plugins;
using HyperaiShell.Foundation;
using HyperaiShell.Foundation.Data;
using HyperaiShell.Foundation.Plugins;
using HyperaiShell.Foundation.Services;
using LiteDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NuGet.Packaging;
using Ac682.Extensions.Logging.Console;
using Sentry;
using PluginManager = HyperaiShell.App.Plugins.PluginManager;

namespace HyperaiShell.App
{
    public class Program
    {
        private static ILogger _logger;
        public async static Task Main()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            // manager instance init
            PackageManager.Instance.PluginPackageLoaded = PluginPackageLoaded;

            //env init
            var cfgBuilder = new ConfigurationBuilder().AddTomlFile("appsettings.toml", false);
            var config = cfgBuilder.Build();

            var startup = new Bootstrapper(config);
            var hostBuilder = new HostBuilder()
                .ConfigureServices(startup.ConfigureServices)
                .UseConsoleLifetime();
            // search packages and load
            var nupkgs = new Queue<string>();
            foreach (var nupkg in Directory.GetFiles("plugins", "*.nupkg"))
            {
                nupkgs.Enqueue(nupkg);
            }

            await PackageManager.Instance.BeginBatchAsync(nupkgs);

            PluginManager.Instance.ActivateAll(plugin => hostBuilder.ConfigureServices(plugin.ConfigureServices));

            Shared.Host = hostBuilder.Build();

            _logger = Shared.Host.Services.GetRequiredService<ILogger<Program>>();

            Welcome();

            var botService = Shared.Host.Services.GetRequiredService<IBotService>();
            var unitService = Shared.Host.Services.GetRequiredService<IUnitService>();
            unitService.SearchForUnits();

            PluginManager.Instance.ActivateAll(plugin =>
            {
                plugin.ConfigureBots(botService.Builder, config);
                plugin.OnStarted(Shared.Host.Services, config);
                _logger.LogInformation("Plugin {PluginIdentity}/{Version} activated", plugin.Context.Meta.Identity,
                    plugin.GetType().Assembly.GetName().Version);
            });

            var task = Shared.Host.RunAsync();
            // TEST HERE

            await task;
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = (Exception) e.ExceptionObject;
            if (e.IsTerminating)
            {
                _logger.LogCritical(exception, "Terminating for uncaught exception");
                Environment.ExitCode = -1;
            }
            else
            {
                _logger.LogError(exception, "Exception caught");
            }
        }

        private static void Welcome()
        {
            _logger.LogInformation(@"
 ____            _ _   _                            _ 
|  _ \ _ __ ___ (_) | | |_   _ _ __   ___ _ __ __ _(_)
| |_) | '__/ _ \| | |_| | | | | '_ \ / _ \ '__/ _` | |
|  __/| | | (_) | |  _  | |_| | |_) |  __/ | | (_| | |
|_|   |_|  \___// |_| |_|\__, | .__/ \___|_|  \__,_|_|
              |__/       |___/|_|                     ");
            _logger.LogInformation(
                "Powered by ProjHyperai\nHyperaiShell v{HyperaiShellV} (Plugin based on v{PluginBaseV})\nHyperai v{HyperaiV}\nHyperai.Units v{HyperaiUnitsV}",
                typeof(Program).Assembly.GetName().Version,
                typeof(PluginBase).Assembly.GetName().Version,
                typeof(IApiClient).Assembly.GetName().Version,
                typeof(UnitService).Assembly.GetName().Version);
        }

        private static async void PluginPackageLoaded(string fileName, PackageArchiveReader reader,
            IEnumerable<Assembly> assemblies)
        {
            var plugins = assemblies.SelectMany(x =>
                x.GetExportedTypes().Where(y => !y.IsAbstract && y.IsSubclassOf(typeof(PluginBase))));
            var plugin = plugins.FirstOrDefault(); // 其他的都无视掉

            if (plugin != null)
            {
                var context = new PluginContext();
                var identity = await reader.GetIdentityAsync(CancellationToken.None);
                var meta = new PluginMeta(identity.Id, fileName,
                    Path.Combine("plugins", identity.Id));
                context.Meta = meta;
                if (!Directory.Exists(meta.SpaceDirectory))
                {
                    Directory.CreateDirectory(meta.SpaceDirectory);

                    var content = (await reader.GetContentItemsAsync(CancellationToken.None))
                        .OrderByDescending(x => x.TargetFramework.Version).FirstOrDefault();

                    if (content != null)
                    {
                        foreach (var item in content.Items)
                        {
                            await using var contentStream = await reader.GetStreamAsync(item, CancellationToken.None);
                            await using var fileStream = File.OpenWrite(Path.Combine(meta.SpaceDirectory,
                                item.Substring(item.IndexOf('/') + 1)));
                            await contentStream.CopyToAsync(fileStream);
                            await fileStream.FlushAsync();
                        }
                    }
                }
                var configFile = Path.Combine(meta.SpaceDirectory, "config.toml");
                if (File.Exists(configFile))
                    context.Configuration =
                        new Lazy<IConfiguration>(() => new ConfigurationBuilder().AddTomlFile(configFile).Build());
                else context.Configuration = new Lazy<IConfiguration>(() => new ConfigurationBuilder().Build());

                var dataFile = Path.Combine(meta.SpaceDirectory, "data.lite.db");
                context.Repository = new Lazy<IRepository>(() => new LiteDbRepository(new LiteDatabase(dataFile)));
                PluginManager.Instance.RegisterPlugin(plugin, context);
            }
        }
    }
}
