namespace Hyperai.Receipts
{
    public class MessageReceipt : GenericReceipt
    {
        public long MessageId
        {
            get => (long) this[nameof(MessageId)];
            set => this[nameof(MessageId)] = value;
        }
    }
}
