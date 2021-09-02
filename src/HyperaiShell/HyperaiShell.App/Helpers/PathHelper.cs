using System;

namespace HyperaiShell.App.Helpers
{
    public static class PathHelper
    {
        public static string Absolute2Relative(string rel)
        {
            return Absolute2Relative(
                Environment.CurrentDirectory.EndsWith('/') || Environment.CurrentDirectory.EndsWith('\\')
                    ? Environment.CurrentDirectory
                    : Environment.CurrentDirectory + '/', rel);
        }

        public static string Absolute2Relative(string b, string r)
        {
            return new Uri(new Uri(b, UriKind.Absolute), r).LocalPath;
        }

        public static string ToAbsolutePath(this string path)
        {
            return Absolute2Relative(path);
        }
    }
}
