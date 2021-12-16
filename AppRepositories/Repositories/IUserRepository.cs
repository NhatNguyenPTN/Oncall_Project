using EFCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public interface IUserRepository: IGenericRepository<User>
    {
        public int a();
    }
}
