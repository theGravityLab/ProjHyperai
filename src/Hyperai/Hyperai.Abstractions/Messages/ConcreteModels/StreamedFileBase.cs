using System.IO;
using Hyperai.Messages.ConcreteModels.FileSources;

namespace Hyperai.Messages.ConcreteModels
{
    public abstract class StreamedFileBase : MessageElement
    {
        public IFileSource Source { get; set; }

        public Stream OpenRead()
        {
            return Source.OpenRead();
        }

        public override int GetHashCode()
        {
            return 0;
            // File 都不能比较，判定相等
        }
    }
}
