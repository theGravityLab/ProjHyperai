using System.Collections.Generic;
using Hyperai.Messages;
using Hyperai.Messages.ConcreteModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hyperai.Abstractions.Tests.Messages
{
    [TestClass]
    public class MessageChainTests
    {
        [TestMethod]
        public void ChainEquals_WithSame_ReturnsTrue()
        {
            // Arrange
            var c1 = new MessageChain(new List<MessageElement> {new Plain("While I am a cat.")});
            var c2 = new MessageChain(new List<MessageElement> {new Plain("While I am a cat.")});
            // Act
            var assert = c1.ChainEquals(c2);
            // Assert
            Assert.IsTrue(assert);
        }

        [TestMethod]
        public void ChainEquals_WithOther_ReturnsFalse()
        {
            // Arrange
            var c1 = new MessageChain(new List<MessageElement> {new Plain("While I am a cat.")});
            var c2 = new MessageChain(new List<MessageElement> {new Plain("While I am a dog.")});
            // Act
            var assert = c1.ChainEquals(c2);
            // Assert
            Assert.IsFalse(assert);
        }

        [TestMethod]
        public void ChainEquals_WithNull_ReturnsFalse()
        {
            // Arrange
            var c1 = new MessageChain(new List<MessageElement> {new Plain("While I am a cat.")});
            // Act
            var assert = c1.ChainEquals(null);
            // Assert
            Assert.IsFalse(assert);
        }
    }
}
