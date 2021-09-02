using Hyperai.Messages.ConcreteModels.FileSources;

namespace Hyperai.Messages.ConcreteModels
{
    public class Video: StreamedFileBase
    {
        public Video(IFileSource source)
        {
            Source = source;
        }
    }
}
