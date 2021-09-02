using Hyperai.Messages.ConcreteModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hyperai.Abstractions.Tests.Messages
{
    [TestClass]
    public class PlainTests
    {
        [TestMethod]
        public void Equals_WithNull_ReturnsFalse()
        {
            // Arrange
            var p1 = new Plain("a dog");
            // Act
            var assert = p1.Equals(null);
            // Assert
            Assert.IsFalse(assert);
        }

        [TestMethod]
        public void Equals_WithOther_ReturnsFalse()
        {
            // Arrange
            var p1 = new Plain("a dog");
            var p2 = new Plain("a cat");
            // Act
            var assert = p1.Equals(p2);
            // Assert
            Assert.IsFalse(assert);
        }

        [TestMethod]
        public void Equals_WithSame_ReturnsTrue()
        {
            // Arrange
            var p1 = new Plain("a dog");
            var p2 = new Plain("a dog");
            // Act
            var assert = p1.Equals(p2);
            // Assert
            Assert.IsTrue(assert);
        }

        [TestMethod]
        public void EqualsOperator_AnyAndNull_ReturnsFalse()
        {
            // Arrange
            var p1 = new Plain("a dog");
            // Act
            var assert = p1 == null;
            // Assert
            Assert.IsFalse(assert);
        }

        [TestMethod]
        public void EqualsOperator_NullAndAny_ReturnsFalse()
        {
            // Arrange
            var p1 = new Plain("a dog");
            // Act
            var assert = null == p1;
            // Assert
            Assert.IsFalse(assert);
        }

        [TestMethod]
        public void EqualsOperator_OneAndOne_ReturnsTrue()
        {
            // Arrange
            var p1 = new Plain("a dog");
            var p2 = new Plain("a dog");
            // Act
            var assert = p1 == p2;
            // Assert
            Assert.IsTrue(assert);
        }

        [TestMethod]
        public void EqualsOperator_OneAndOther_ReturnsFalse()
        {
            // Arrange
            var p1 = new Plain("a dog");
            var p2 = new Plain("a cat");
            // Act
            var assert = p1 == p2;
            // Assert
            Assert.IsFalse(assert);
        }
    }
}
