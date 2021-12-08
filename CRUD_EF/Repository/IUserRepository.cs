using CRUD_EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_EF.Repository
{
    public interface IUserRepository<T>
    {
        List<T> GetAllUser();
        T GetUserById(Guid id);
        bool AddUser(T user);
        bool EditUser(Guid id ,T user);
        bool DeleteUser(Guid id);
        bool IsValidName(string fullname);
        bool IsValidNameEdit(Guid id,string fullname);
        public bool IsExistUser(Guid id);
        public bool IsUserListEmty(List<T> userList);
        public List<User> SearchByCondition(UserSearch condition);
       
        
        
    }
}
