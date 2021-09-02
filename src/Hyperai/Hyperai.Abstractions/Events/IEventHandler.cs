namespace Hyperai.Events
{
    public interface IEventHandler<in T> where T : GenericEventArgs
    {
        void Handle(T args);
    }
}
