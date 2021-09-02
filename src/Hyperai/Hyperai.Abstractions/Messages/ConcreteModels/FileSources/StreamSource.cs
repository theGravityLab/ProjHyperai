using System;
using System.IO;

namespace Hyperai.Messages.ConcreteModels.FileSources
{
    public sealed class StreamSource : IFileSource, IDisposable
    {
        public StreamSource(Stream data)
        {
            Data = data;
        }

        public Stream Data { get; set; }

        public void Dispose()
        {
            Data?.Dispose();
        }

        public Stream OpenRead()
        {
            return Data;
        }
    }
}
