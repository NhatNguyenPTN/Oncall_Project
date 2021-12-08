using CRUD_EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_EF.Repository
{
    public interface IUserLoginRepository
    {
        public bool IsExistUser(string fullname);

        public bool IsTrueEmail(UserLogin user);

        List<User> GetAllUser();
        public string GenerateToken(UserLogin user);


    }
}
