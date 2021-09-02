using System.IO;
using Hyperai.Messages.ConcreteModels.FileSources;

namespace Hyperai.Messages.ConcreteModels
{
    public class Voice: StreamedFileBase
    {
        public Voice(IFileSource source)
        {
            Source = source;
        }
    }
}
