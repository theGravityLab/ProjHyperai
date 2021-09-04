namespace Hyperai.Messages.ConcreteModels
{
    public abstract class ImageBase : StreamedFileBase
    {
        public string ImageId { get; set; }

        public override int GetHashCode()
        {
            return ImageId.GetHashCode();
        }


        public override string ToString()
        {
            return $"<{GetType().Name.ToUpper()} {ImageId}>";
        }
    }
}
