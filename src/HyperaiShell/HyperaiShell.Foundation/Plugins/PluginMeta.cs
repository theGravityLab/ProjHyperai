namespace HyperaiShell.Foundation.Plugins
{
    public readonly struct PluginMeta
    {
        public string Identity { get; }
        public string FileBase { get; }
        public string SpaceDirectory { get; }

        public PluginMeta(string identity, string fileBase, string configDirectory)
        {
            Identity = identity;
            FileBase = fileBase;
            SpaceDirectory = configDirectory;
        }
    }
}
