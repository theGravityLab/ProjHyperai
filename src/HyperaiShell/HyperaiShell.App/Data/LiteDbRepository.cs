using HyperaiShell.Foundation.Data;
using LiteDB;

namespace HyperaiShell.App.Data
{
    public class LiteDbRepository : IRepository
    {
        private readonly LiteRepository _repository;

        public LiteDbRepository(LiteDatabase database)
        {
            _repository = new LiteRepository(database);
        }

        public void Store<T>(T ins)
        {
            _repository.Insert(ins);
        }

        public bool Delete<T>(object id)
        {
            return _repository.Delete<T>(new BsonValue(id));
        }

        public ILiteQueryable<T> Query<T>()
        {
            return _repository.Query<T>();
        }

        public bool Update<T>(T ins)
        {
            return _repository.Update(ins);
        }

        public bool Upsert<T>(T ins)
        {
            return _repository.Upsert(ins);
        }
    }
}
