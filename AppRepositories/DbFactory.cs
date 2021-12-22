using EFCore.DbConnection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class DbFactory : IDisposable
    {
        private bool _disposed;
        private readonly Func<UserContext> _dbContextFactory;
        private DbContext _dbContext;
        public DbContext DbContext => _dbContext ??= _dbContextFactory.Invoke();
        public DbFactory(Func<UserContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public void Dispose()
        {
            if (!_disposed && _dbContext != null)
            {
                _disposed = true;
                _dbContext.Dispose();
            }
        }
    }
}

