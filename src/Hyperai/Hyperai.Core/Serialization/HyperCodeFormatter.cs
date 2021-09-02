using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Hyperai.Messages;
using Hyperai.Messages.ConcreteModels;
using Hyperai.Messages.ConcreteModels.FileSources;

namespace Hyperai.Serialization
{
    public class HyperCodeFormatter : IMessageChainFormatter
    {
        public string Format(MessageChain chain)
        {
            return string.Join(string.Empty,
                chain.Where(x => x.GetType().GetCustomAttribute<SerializableAttribute>() != null)
                    .Select(PlainSelector));

            string PlainSelector(MessageElement comp)
            {
                return comp switch
                {
                    Plain it => Escape(it.Text),
                    _ => $"[hyper.{comp.TypeName.ToLower()}({CodeSelector(comp)})]"
                };
            }

            string CodeSelector(MessageElement comp)
            {
                return comp switch
                {
                    At it => it.TargetId.ToString(),
                    AtAll it => string.Empty,
                    Face it => it.FaceId.ToString(),
                    ImageBase {Source: UrlSource} it =>
                        $"{it.ImageId},{((UrlSource) it.Source).Url.AbsoluteUri}",
                    Poke it => it.Name.ToString(),
                    Quote it => it.MessageId.ToString(),
                    Source it => it.MessageId.ToString(),
                    Music it => $"{it.Type},{it.MusicId}",

                    StreamedFileBase {Source: UrlSource} it => $"{((UrlSource)it.Source).Url.AbsoluteUri}",

                    _ => throw new NotImplementedException()
                };
            }
        }

        public string Escape(string text)
        {
            StringBuilder sb = new();
            var addedLength = 0;

            var matches = HyperCodeParser.HyperRegex.Matches(text);
            foreach (Match match in matches)
            {
                var count = HyperCodeParser.CountBefore(match.Index, text);
                sb.Append(text[addedLength..match.Index]);
                sb.Append('\\', count * 2 + 1);
                sb.Append(match.Value);
                addedLength = match.Index + match.Length;
            }

            if (addedLength < text.Length) sb.Append(text[addedLength..]);
            return sb.ToString();
        }
    }
}
