using System.Text;
using System.Text.RegularExpressions;
using Hyperai.Messages;

namespace Hyperai.Serialization
{
    public class HyperCodeParser : IMessageChainParser
    {
        internal static readonly Regex HyperRegex =
            new(@"\[hyper\.(?<name>[a-z]+)\((?<code>[a-z0-9A-Z_\\:/,.@\-=?&#{}\ ]*)\)\]");

        public MessageChain Parse(string text)
        {
            var builder = new MessageChainBuilder();
            var matches = HyperRegex.Matches(text);

            var addedLength = 0;

            foreach (Match match in matches)
            {
                if (!Validate(match.Index, text)) continue;
                var plain = text[addedLength..match.Index];
                if (!string.IsNullOrEmpty(plain)) builder.AddPlain(Unescape(plain));
                builder.Add(MessageElementFactory.Produce(match.Groups["name"].Value, match.Groups["code"].Value));
                addedLength = match.Index + match.Length;
            }

            if (addedLength < text.Length) builder.AddPlain(Unescape(text[addedLength..]));
            return builder.Build();
        }

        private static string Unescape(string text)
        {
            StringBuilder sb = new();
            var addedLength = 0;

            var matches = HyperRegex.Matches(text);
            foreach (Match match in matches)
            {
                var count = CountBefore(match.Index, text);
                sb.Append(text[addedLength..(match.Index - (count / 2 + 1))]);
                sb.Append(match.Value);
                addedLength = match.Index + match.Length;
            }

            if (addedLength < text.Length) sb.Append(text[addedLength..]);
            return sb.ToString();
        }

        internal static bool Validate(int pos, string text)
        {
            return CountBefore(pos, text) % 2 == 0;
        }

        internal static int CountBefore(int pos, string text)
        {
            // pos 是左括号的位置
            var start = pos;
            while (start > 0 && text[start - 1] == '\\') start--;
            return pos - start;
        }
    }
}
