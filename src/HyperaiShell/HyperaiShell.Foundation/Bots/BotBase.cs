using Hyperai.Events;
using Hyperai.Relations;
using Hyperai.Services;

namespace HyperaiShell.Foundation.Bots
{
    public abstract class BotBase
    {
        public virtual Self Me { get; set; }

        public virtual void OnEverything(object sender, GenericEventArgs args)
        {
        }

        public virtual void OnFriendMessage(object client, FriendMessageEventArgs args)
        {
        }

        public virtual void OnGroupMessage(object client, GroupMessageEventArgs args)
        {
        }

        public virtual void OnFriendRecall(object client, FriendRecallEventArgs args)
        {
        }

        public virtual void OnGroupRecall(object client, GroupRecallEventArgs args)
        {
        }

        public virtual void OnGroupLeft(object client, GroupLeftEventArgs args)
        {
        }

        public virtual void OnGroupJoined(object client, GroupJoinedEventArgs args)
        {
        }

        public virtual void OnGroupMemberMuted(object client, GroupMemberMutedEventArgs args)
        {
        }

        public virtual void OnGroupMemberUnmuted(object client, GroupMemberUnmutedEventArgs args)
        {
        }

        public virtual void OnGroupAllMuted(object client, GroupAllMutedEventArgs args)
        {
        }

        public virtual void OnGroupMemberCardChanged(object client, GroupMemberCardChangedEventArgs args)
        {
        }

        public virtual void OnGroupMemberTitleChanged(object client, GroupMemberTitleChangedEventArgs args)
        {
        }

        public virtual void OnGroupPermissionChanged(object client, GroupPermissionChangedEventArgs args)
        {
        }

        public virtual void OnGroupRequest(object client, GroupRequestEventArgs args)
        {
        }

        public virtual void OnFriendRequest(object client, FriendRequestEventArgs args)
        {
        }
    }
}
