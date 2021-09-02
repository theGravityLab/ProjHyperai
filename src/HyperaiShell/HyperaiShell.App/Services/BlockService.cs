using System;
using HyperaiShell.App.Models;
using HyperaiShell.Foundation.Data;
using HyperaiShell.Foundation.Services;

namespace HyperaiShell.App.Services
{
    public class BlockService : IBlockService
    {
        private readonly IRepository _repository;

        public BlockService(IRepository repository)
        {
            _repository = repository;
        }

        public void Ban(long id, string reason)
        {
            _repository.Upsert(new BlockedUser
                {UserId = id, Reason = reason, Enrollment = DateTime.Now, IsBanned = true});
        }

        public void Deban(long id)
        {
            var user = _repository.Query<BlockedUser>().Where(x => x.UserId == id).FirstOrDefault();
            if (user != null)
            {
                user.IsBanned = false;
                _repository.Update(user);
            }
        }

        public bool IsBanned(long id, out string reason)
        {
            var user = _repository.Query<BlockedUser>().Where(x => x.UserId == id).FirstOrDefault();
            if (user == null)
            {
                reason = null;
                return false;
            }

            reason = user.IsBanned ? user.Reason : null;
            return user.IsBanned;
        }
    }
}
