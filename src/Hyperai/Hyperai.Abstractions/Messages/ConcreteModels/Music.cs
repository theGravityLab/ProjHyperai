namespace Hyperai.Messages.ConcreteModels
{
    public class Music : MessageElement
    {
        public enum MusicSource
        {
            QqMusic,
            Music163,
            XiaMi
        }

        public Music(MusicSource type, string id)
        {
            Type = type;
            MusicId = id;
        }

        public string MusicId { get; set; }

        public MusicSource Type { get; set; }

        public override int GetHashCode()
        {
            return MusicId.GetHashCode();
        }
    }
}
