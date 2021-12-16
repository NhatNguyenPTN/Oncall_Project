using EFCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbFactory dbFactory): base (dbFactory)
        {

        }
        public int a() {
            return 1;
        }

    }
}
