namespace Hyperai.Relations
{
    public class Signature
    {
        public Signature(string expression)
        {
            Expression = expression;
        }

        public string Expression { get; set; }
        public long Destination { get; set; }

        public bool Match(Member member)
        {
            var prefix = Expression.Substring(0, Expression.IndexOf(':'));
            var postfix = Expression.Substring(prefix.Length + 1);

            if (prefix == "*")
            {
                if (long.TryParse(postfix, out var result)) return result == member.Identity;

                return false;
            }

            return prefix == member.Group.Value.Identity.ToString() && (postfix == "*" ||
                                                                        member.Group.Value.Identity.ToString() ==
                                                                        prefix &&
                                                                        member.Identity.ToString() == postfix);
        }

        public bool Match(Friend friend)
        {
            var prefix = Expression.Substring(0, Expression.IndexOf(':'));
            var postfix = Expression.Substring(prefix.Length + 1);

            if (prefix == "*") return false;
            if (prefix == "_")
            {
                if (long.TryParse(postfix, out var result)) return result == friend.Identity;

                return postfix == "*";
            }

            return false;
        }

        public static Signature FromGroup(long groupId)
        {
            return new($"{groupId}:*");
        }

        public static Signature FromMember(long groupId, long memberId)
        {
            return new($"{groupId}:{memberId}");
        }

        public static Signature FromAnyGroup(long userId)
        {
            return new($"*:{userId}");
        }

        public static Signature FromAnyGroupAnyMember()
        {
            return new("*:*");
        }

        public static Signature FromFriend(long friendId)
        {
            return new($"_:{friendId}");
        }

        public static Signature FromAnyFriend()
        {
            return new("_:*");
        }
    }
}
