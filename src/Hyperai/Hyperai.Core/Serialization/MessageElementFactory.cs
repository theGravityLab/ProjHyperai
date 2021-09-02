using System;
using Hyperai.Messages;
using Hyperai.Messages.ConcreteModels;
using Hyperai.Messages.ConcreteModels.FileSources;

namespace Hyperai.Serialization
{
    public static class MessageElementFactory
    {
        public static MessageElement Produce(string name, string code)
        {
            return name.ToLower() switch
            {
                "at" => new At(Convert.ToInt64(code)),
                "atall" => new AtAll(),
                "face" => new Face(Convert.ToInt32(code)),
                "flash" => new Flash(code.Substring(0, code.IndexOf(',')),
                    new UrlSource(new Uri(code[(code.IndexOf(',') + 1)..], UriKind.Absolute))),
                "image" => new Image(code.Substring(0, code.IndexOf(',')),
                    new UrlSource(new Uri(code[(code.IndexOf(',') + 1)..], UriKind.Absolute))),
                "plain" => new Plain(code),
                "poke" => new Poke(Enum.Parse<PokeType>(code)),
                "quote" => new Quote(Convert.ToInt32(code)),
                "source" => new Source(Convert.ToInt32(code)),
                "video" => new Video(new UrlSource(new Uri(code, UriKind.Absolute))),
                "voice" => new Voice(new UrlSource(new Uri(code, UriKind.Absolute))),
                "muisc" => new Music(Enum.Parse<Music.MusicSource>(code.Substring(0, code.IndexOf(','))), code[(code.IndexOf(',') + 1)..]),

                _ => throw new NotImplementedException()
            };
        }
    }
}
