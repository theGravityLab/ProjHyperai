using System.IO;
using System.Threading.Tasks;

namespace Hyperai.Messages.ConcreteModels
{
    public static class ImageBaseExtensions
    {
        public static async Task<Stream> OpenReadAsync(this ImageBase image)
        {
            return await Task.Run(image.OpenRead);
        }
    }
}
