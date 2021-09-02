using System.IO;
using Ac682.Extensions.Logging.Console;
using Hangfire;
using Hangfire.Storage.SQLite;
using Hyperai.Messages;
using Hyperai.Serialization;
using Hyperai.Units;
using HyperaiShell.App.Data;
using HyperaiShell.App.Hangfire.Logging;
using HyperaiShell.App.Logging.ConsoleFormatters;
using HyperaiShell.App.Plugins;
using HyperaiShell.Foundation.Data;
using HyperaiShell.Foundation.Plugins;
using LiteDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using HyperaiShell.App.Services;
using Sentry.Extensions.Logging.Extensions.DependencyInjection;
using HyperaiShell.App.Logging;
using Hyperai;
using HyperaiShell.App.Middlewares;

namespace HyperaiShell.App
{
    public class Bootstrapper
    {
        public IConfiguration Configuration { get; private set; }

        public Bootstrapper(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            var dbName = "data/internal.litedb.db";
            Directory.CreateDirectory(Path.GetDirectoryName(dbName)!);
            var database = new LiteDatabase(dbName);
            var repository = new LiteDbRepository(database);

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IRepository>(repository);
            
            services.AddLogging(builder =>
            {
                builder
                    .AddConfiguration(Configuration)
                    .AddDebug()
                    .AddFile("logs/app.log")
                    .AddConsole(c => c
                        .SetMinimalLevel(LogLevel.Debug)
                        .AddBuiltinFormatters()
                        .AddFormatter<MessageElementFormatter>()
                        .AddFormatter<RelationFormatter>()
                        .AddFormatter<EventArgsFormatter>()
                    );
                
                    builder.AddSentry();
            });

            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                TypeNameHandling = TypeNameHandling.All
            };

            services.AddScoped(typeof(IPluginConfiguration<>), typeof(PluginConfiguration<>))
                .AddScoped(typeof(IPluginRepository<>), typeof(PluginRepository<>))
                .AddScoped<IMessageChainFormatter, HyperCodeFormatter>()
                .AddScoped<IMessageChainParser, HyperCodeParser>()
                .AddHangfire(configure => configure
                    .UseLogProvider(new HangfireLogProvider())
                    .UseSQLiteStorage("data/hangfire.sqlite.db")
                    .UseSerializerSettings(settings))
                .AddHttpClient()
                .AddHangfireServer()
                .AddHyperaiServer(options => options
                    .UseLogging()
                    .UseBlacklist()
                    .UseTranslator()
                    .UseBots()
                    .UseUnits())
                .AddDistributedMemoryCache()
                .AddBots()
                .AddClients(Configuration)
                .AddUnits()
                .AddAttachments()
                .AddAuthorizationService()
                .AddBlacklist();
        }
    }
}
