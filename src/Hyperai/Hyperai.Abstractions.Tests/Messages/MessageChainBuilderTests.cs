using System.Collections.Generic;
using Hyperai.Messages;
using Hyperai.Messages.ConcreteModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hyperai.Abstractions.Tests.Messages
{
    [TestClass]
    public class MessageChainBuilderTests
    {
        [TestMethod]
        public void Build_SameComponents_ReturnsRightChain()
        {
            // Arrange
            var builder = new MessageChainBuilder();
            builder.AddPlain("While I am a cat.");
            var comp = new MessageChain(new List<MessageElement> {new Plain("While I am a cat.")});
            // Act
            var chain = builder.Build();
            // Assert
            Assert.IsTrue(chain.ChainEquals(comp));
        }
    }
}
