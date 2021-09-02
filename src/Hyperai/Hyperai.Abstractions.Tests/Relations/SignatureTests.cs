using System;
using Hyperai.Relations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hyperai.Abstractions.Tests.Relations
{
    [TestClass]
    public class RelationMatcherTests
    {
        [TestMethod]
        public void FriendAndFriend_True()
        {
            var friend = new Friend {Identity = 123};

            var matcher = Signature.FromFriend(friend.Identity);

            Assert.IsTrue(matcher.Match(friend));
        }

        [TestMethod]
        public void FriendAndMember_False()
        {
            var friend = new Friend {Identity = 123};

            var matcher = Signature.FromMember(321, friend.Identity);

            Assert.IsFalse(matcher.Match(friend));
        }

        [TestMethod]
        public void MemberAndMember_True()
        {
            var member = new Member {Group = new Lazy<Group>(new Group {Identity = 321}), Identity = 123};

            var matcher = Signature.FromMember(member.Group.Value.Identity, member.Identity);

            Assert.IsTrue(matcher.Match(member));
        }

        [TestMethod]
        public void AnyMember_True()
        {
            var member = new Member {Group = new Lazy<Group>(new Group {Identity = 321}), Identity = 123};

            var matcher = Signature.FromGroup(321);

            Assert.IsTrue(matcher.Match(member));
        }

        [TestMethod]
        public void AnyMember_False()
        {
            var member = new Member {Group = new Lazy<Group>(new Group {Identity = 321}), Identity = 123};

            var matcher = Signature.FromGroup(233);

            Assert.IsFalse(matcher.Match(member));
        }
    }
}
