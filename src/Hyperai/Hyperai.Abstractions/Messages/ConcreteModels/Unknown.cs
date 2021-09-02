namespace Hyperai.Messages.ConcreteModels
{
    public class Unknown : MessageElement
    {
        public Unknown(string data)
        {
            Data = data;
        }

        public string Data { get; set; }

        public override int GetHashCode()
        {
            return Data.GetHashCode();
        }
    }
}
