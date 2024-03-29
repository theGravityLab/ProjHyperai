using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Common;
using NuGet.Frameworks;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;

namespace HyperaiShell.App.Packages
{
    public class PackageManager
    {
        private readonly SourceCacheContext cacheContext = new();


        private readonly List<string> loadedIdentities = new();

        private readonly SourceRepository repository =
            Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");

        public Action<string, PackageArchiveReader, IEnumerable<Assembly>> PluginPackageLoaded { get; set; }


        public async Task BeginBatchAsync(IEnumerable<string> files)
        {
            var queue = new Queue<string>();
            foreach (var file in files) queue.Enqueue(file);

            var tCount = queue.Count;
            var nCount = 0;
            while (queue.Count > 0)
            {
                var now = queue.Dequeue();
                var (reader, assemblies) =
                    nCount < tCount
                        ? await LoadPluginPackageAsync(now)
                        : await LoadDependencyPackageAsync(now);

                if (reader == null) continue;

                var depGroup = (await reader.GetPackageDependenciesAsync(CancellationToken.None))
                    .OrderByDescending(x => x.TargetFramework.Version).FirstOrDefault();

                foreach (var dependent in depGroup?.Packages ?? Enumerable.Empty<PackageDependency>())
                {
                    if (loadedIdentities.Contains(dependent.Id)) continue;
                    var located = await LocatePackageAsync(dependent.Id, dependent.VersionRange.MinVersion.Version);
                    if (string.IsNullOrWhiteSpace(located)) continue;

                    queue.Enqueue(located);
                }

                nCount++;
            }
        }

        public async Task<(PackageArchiveReader, IEnumerable<Assembly>)> LoadPluginPackageAsync(string file)
        {
            await using var stream = File.OpenRead(file);
            var (reader, assemblies) = await LoadPackageAsync(stream);
            if (reader != null) OnPluginPackageLoaded(file, reader, assemblies);

            return (reader, assemblies);
        }

        public async Task<(PackageArchiveReader, IEnumerable<Assembly>)> LoadDependencyPackageAsync(string file)
        {
            await using var stream = File.OpenRead(file);
            var (reader, assemblies) = await LoadPackageAsync(stream);

            return (reader, assemblies);
        }

        public async Task<(PackageArchiveReader, IEnumerable<Assembly>)> LoadPackageAsync(Stream stream)
        {
            var reader = new PackageArchiveReader(stream);
            var identity = await reader.GetIdentityAsync(CancellationToken.None);
            if (CheckIfNoNeed(identity.Id)) return (null, null);

            var assemblies = new List<Assembly>();
            var libGroup = (await reader.GetLibItemsAsync(CancellationToken.None))
                .OrderByDescending(x => x.TargetFramework, new NuGetFrameworkSorter()).FirstOrDefault();
            foreach (var item in libGroup?.Items ?? Enumerable.Empty<string>())
            {
                if (!item.EndsWith(".dll", StringComparison.CurrentCultureIgnoreCase)) continue;
                if (item.Count(x => x == '/' || x == '\\') > 2) continue; // skip net50/*culture*/*.resource.dll
                var libStream = reader.GetStream(item);
                var buffer = new MemoryStream();
                await libStream.CopyToAsync(buffer);
                buffer.Position = 0;
                var bytes = new byte[buffer.Length];
                buffer.Read(bytes, 0, bytes.Length);
                var assembly = Assembly.Load(bytes);
                assemblies.Add(assembly);
                buffer.Close();
                libStream.Close();
            }

            loadedIdentities.Add(identity.Id);
            return (reader, assemblies);
        }

        public Assembly FindAssembly(string assemblyName)
        {
            foreach (var assembly in AssemblyLoadContext.All.SelectMany(x => x.Assemblies))
                if (assembly.GetName().Name == new AssemblyName(assemblyName).Name)
                    return assembly;

            return null;
        }

        protected void OnPluginPackageLoaded(string file, PackageArchiveReader reader, IEnumerable<Assembly> assemblies)
        {
            PluginPackageLoaded?.Invoke(file, reader, assemblies);
        }


        private bool CheckIfNoNeed(string identity)
        {
            return loadedIdentities.Contains(identity);
        }

        private async Task<string> LocatePackageAsync(string identity, Version version)
        {
            // look up in cacahe
            if (CheckIfNoNeed(identity)) return null;
            var packagesDir = Path.Combine(Environment.CurrentDirectory, "cache", "packages");
            Directory.CreateDirectory(packagesDir);
            var file = Path.Combine("cache", "packages", $"{identity}.{version}.nupkg");
            if (!File.Exists(file))
            {
                var resource = await repository.GetResourceAsync<FindPackageByIdResource>(CancellationToken.None);
                var found =
                    (await resource.GetAllVersionsAsync(identity, cacheContext, NullLogger.Instance,
                        CancellationToken.None)).FirstOrDefault(x => x.Version == version);
                if (version != null)
                {
                    var local = Path.Combine("cache", "packages",
                        $"{identity}.{found.Version}.nupkg");
                    var localStream = File.OpenWrite(local);
                    await resource.CopyNupkgToStreamAsync(identity, found, localStream,
                        cacheContext, NullLogger.Instance, CancellationToken.None);
                    await localStream.FlushAsync();
                    localStream.Close();
                    file = local;
                }
            }

            return file;
        }

        #region Singleton

        public static PackageManager Instance { get; } = new();

        private PackageManager()
        {
        }

        #endregion
    }
}
