using System.Linq;
using Hyperai.Units.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hyperai.Units.Abstractions.Tests.Attributes
{
    [TestClass]
    public class ExtractAttributeTests
    {
        [TestMethod]
        public void Ctor_GenerateNames()
        {
            // A & A
            var attr = new ExtractAttribute("!at [hyper.at({who})]");
            // A
            Assert.IsTrue(attr.Names.SequenceEqual(new[] {"who"}));
        }

        [TestMethod]
        public void Regex_Match()
        {
            // A & A
            var attr = new ExtractAttribute("!ban {ban}");
            var match = attr.Pattern.Match("!ban me!");
            // A
            Assert.IsTrue(match.Success);
        }
    }
}
