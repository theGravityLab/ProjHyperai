using System;
using System.IO;
using Hyperai.Messages.ConcreteModels.FileSources;

namespace Hyperai.Messages.ConcreteModels
{
    [Serializable]
    public class Image : ImageBase
    {
        public Image(string imageId, IFileSource source)
        {
            ImageId = imageId;
            Source = source;
        }

        public static Image FromUrl(string id, Uri url)
        {
            return new Image(id, new UrlSource(url));
        }

        public static Image FromStream(string id, Stream stream)
        {
            return new Image(id, new StreamSource(stream));
        }
    }
}
