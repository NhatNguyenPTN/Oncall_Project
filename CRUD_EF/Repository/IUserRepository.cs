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
        T GetUserById(Guid userId);
        bool AddUser(T user);
        bool EditUser(Guid userId, T user);
        bool DeleteUser(Guid id);
        bool IsValidName(string fullname);
        bool IsValidNameEdit(Guid id, string fullname);
        bool IsExistUser(Guid userId);
        bool IsUserListEmty(List<T> userList);
        List<User> SearchByCondition(UserSearch condition);
        Guid CheckFormatGuid(string id);
    }
}
