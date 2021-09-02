using System.IO;

namespace Hyperai.Messages.ConcreteModels.FileSources
{
    public interface IFileSource
    {
        Stream OpenRead();
    }
}
