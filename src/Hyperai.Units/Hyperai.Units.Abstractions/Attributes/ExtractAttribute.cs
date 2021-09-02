using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Hyperai.Units.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ExtractAttribute : Attribute
    {
        /// <summary>
        ///     创建一个模板用于匹配消息
        /// </summary>
        /// <param name="pattern">模板</param>
        /// <param name="trimSpaces">是否裁剪前后空格和合并二个空格为一个</param>
        public ExtractAttribute(string pattern, bool trimSpaces = false)
        {
            TrimSpaces = trimSpaces;
            RawString = pattern;
            var parameters = Regex.Matches(pattern, @"\{(?<name>[A-Za-z0-9_]+)\}");
            Names = parameters.Select(x => x.Groups["name"].Value).ToList();
            pattern = '^' + Regex.Escape(pattern).Replace(@"\*", ".*").Replace(@"\{", "{") + '$';
            // pattern = Regex.Replace(pattern, @"\{([A-Za-z0-9_]+)\}", @"([\S]+)");
            var nameMatches = Regex.Matches(pattern, @"\{([A-Za-z0-9_]+)\}");
            var patternBuilder = new StringBuilder();
            var addedLength = 0;
            for (var i = 0; i < nameMatches.Count; i++)
            {
                var match = nameMatches[i];
                patternBuilder.Append(pattern[addedLength..match.Index]);
                patternBuilder.Append(i == nameMatches.Count - 1 ? @"([\S\s]+)" : @"([\S]+)");
                addedLength = match.Index + match.Length;
            }

            if (addedLength < pattern.Length) patternBuilder.Append(pattern[addedLength..]);
            Pattern = new Regex(patternBuilder.ToString());
        }

        public Regex Pattern { get; }
        public string RawString { get; }
        public bool TrimSpaces { get; }
        public IList<string> Names { get; }
    }
}
