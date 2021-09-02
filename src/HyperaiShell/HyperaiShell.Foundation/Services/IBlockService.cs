namespace HyperaiShell.Foundation.Services
{
    public interface IBlockService
    {
        void Ban(long id, string reason);

        void Deban(long id);

        bool IsBanned(long id, out string reason);
    }
}
