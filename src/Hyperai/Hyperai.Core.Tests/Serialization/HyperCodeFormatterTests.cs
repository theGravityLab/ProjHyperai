using System;
using Hyperai.Messages;
using Hyperai.Messages.ConcreteModels;
using Hyperai.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hyperai.Core.Tests.Serialization
{
    [TestClass]
    public class HyperCodeFormatterTests
    {
        [TestMethod]
        public void Format_Same()
        {
            // A
            var builder = new MessageChainBuilder();
            builder.Add(new Source(1024));
            builder.AddPlain("World");
            builder.Add(Image.FromUrl("id", new Uri("https://example.com", UriKind.Absolute)));
            builder.AddPlain("Hello");
            var chain = builder.Build();
            var formatter = new HyperCodeFormatter();
            // A
            var res = formatter.Format(chain);

            // A
            Assert.AreEqual("[hyper.source(1024)]World[hyper.image(id,https://example.com/)]Hello", res);
        }


        [TestMethod]
        public void Format_Escape_Same()
        {
            // A
            var builder = new MessageChainBuilder();
            builder.Add(new Source(1024));
            builder.AddPlain(@"[hyper.atall()]");
            builder.AddAtAll();
            var chain = builder.Build();
            var formatter = new HyperCodeFormatter();
            // A
            var res = formatter.Format(chain);

            // A
            Assert.AreEqual(@"[hyper.source(1024)]\[hyper.atall()][hyper.atall()]", res);
        }
    }
}
