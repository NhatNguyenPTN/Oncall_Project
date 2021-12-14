using EFCore.DbConnection;
using EFCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppRepositories
{
    public class UnitOfWork : IDisposable
    {
        public UserContext DbContext { get; }

        public IRepository<User> UserRepository { get; }

        public UnitOfWork(UserContext context, IRepository<User> users)
        {
            DbContext = context;
            UserRepository = users;
            UserRepository.DbContext = DbContext;
        }

        public int SaveChanges()
        {
            var iResult = DbContext.SaveChanges();
            return iResult;
        }
        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
