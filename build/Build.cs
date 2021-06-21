using System;
using System.IO;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[ShutdownDotNetAfterServerBuild]
class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode
    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;
    [Parameter] readonly string ApiKey;

    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            EnsureCleanDirectory(ArtifactsDirectory);
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(c => c
                .SetProcessWorkingDirectory(Solution.Directory));
        });

    Target PublishApplication => _ => _
        .DependsOn(Test)
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetPublish(c => c
                .SetProcessWorkingDirectory(Solution.GetProject("HyperaiShell.App")!.Directory)
                .SetOutput(ArtifactsDirectory)
                .SetRuntime("linux-x64")
                .EnablePublishSingleFile());
        });

    Target GeneratePackages => _ => _
        .DependsOn(Test)
        .DependsOn(Clean)
        .Executes(() =>
        {
            foreach (var project in Solution.AllProjects.Where(x=> x.Name != "_build" && x.Name != "HyperaiShell.App"))
            {
                DotNetPack(c => c
                    .SetProcessWorkingDirectory(project.Directory)
                    .SetOutputDirectory(ArtifactsDirectory));
                
            }
        });

    Target PushPackages => _ => _
        .DependsOn(GeneratePackages)
        .Requires(() => ApiKey)
        .Executes(() =>
        {
            foreach (var file in Directory.GetFiles(ArtifactsDirectory, "*.nupkg"))
            {
                DotNetNuGetPush(c => c
                    .SetSource("https://api.nuget.org/v3/index.json")
                    .SetApiKey(ApiKey)
                    .EnableSkipDuplicate()
                    .SetTargetPath(file));
            }
        });
}
