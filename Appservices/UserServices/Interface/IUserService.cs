using EFCore.Model;
using EFCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appservices.UserServices.Interface
{
    public interface IUserService<T>
    {    
        List<T> GetAllUser();
        UserResponseDto GetAllUser2();
        T GetUserById(Guid userId);
        bool AddUser(T user);
        bool EditUser(Guid userId, T user);
        bool DeleteUser(Guid id);
        bool IsValidName(string fullname);
        bool IsValidNameEdit(Guid id, string fullname);
        bool IsExistUser(Guid userId);
        bool IsUserListEmty(List<T> userList);
        List<User> SearchByCondition(UserSearchRepestDto condition);
        Guid CheckFormatGuid(string id);
    }
}
