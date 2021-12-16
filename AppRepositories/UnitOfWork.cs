using EFCore.Model;
using Infrastructure.Repositories;

namespace Infrastructure
{
    public class UnitOfWork :  IUnitOfWork
    {
            
         public IUserRepository UserRepository { get; }
        public DbFactory DbFactory { get; }
        public UnitOfWork( IUserRepository users, DbFactory dbFactory)
        {           
            UserRepository = users;
            DbFactory = dbFactory;
        }

        public int SaveChanges()
        {
            var iResult = DbFactory.DbContext.SaveChanges();
            return iResult;
        }        
    }
}
