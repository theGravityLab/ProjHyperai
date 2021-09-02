using System;
using System.IO;
using Hyperai.Messages.ConcreteModels.FileSources;

namespace Hyperai.Messages.ConcreteModels
{
    [Serializable]
    public class Flash : ImageBase
    {
        public Flash(string imageId, IFileSource source)
        {
            ImageId = imageId;
            Source = source;
        }
        
        public static Flash FromUrl(string id, Uri url)
        {
            return new(id, new UrlSource(url));
        }

        public static Flash FromStream(string id, Stream stream)
        {
            return new(id, new StreamSource(stream));
        }
    }
}
