using System;

namespace Hyperai.Messages.ConcreteModels
{
    public class Music: MessageElement
    {
        public string MusicId { get; set; }
        
        public MusicSource Type { get; set; }

        public Music(MusicSource type, string id)
        {
            Type = type;
            MusicId = id;
        }

        public override int GetHashCode() => MusicId.GetHashCode();

        public enum MusicSource
        {
            QqMusic,
            Music163,
            XiaMi
        }
    }
}
