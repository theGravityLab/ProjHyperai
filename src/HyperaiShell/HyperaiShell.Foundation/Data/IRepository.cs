using LiteDB;

namespace HyperaiShell.Foundation.Data
{
    public interface IRepository
    {
        void Store<T>(T ins);

        bool Update<T>(T ins);

        bool Delete<T>(object id);

        ILiteQueryable<T> Query<T>();

        bool Upsert<T>(T ins);
    }
}
