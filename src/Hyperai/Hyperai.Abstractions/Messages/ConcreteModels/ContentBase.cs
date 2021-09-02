namespace Hyperai.Messages.ConcreteModels
{
    public class ContentBase : MessageElement
    {
        public string Content { get; set; }

        public override int GetHashCode()
        {
            return Content.GetHashCode();
        }
    }
}
