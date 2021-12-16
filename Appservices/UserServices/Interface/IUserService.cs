using AppServices.UserServices.DTO;
using EFCore.Model;
using System;
using System.Collections.Generic;

namespace Appservices.UserServices.Interface
{
    public interface IUserService<T>
    {    
        List<T> GetAllUser();
        UserResponseListEntityDto GetAllUser2();
        UserResponseEntityDto GetById2(string userId);
        UserResponseListEntityDto SearchByCondition2(UserSearchRepestDto condition);
        UserResponseEntityDto Add2(AddUserRequestDto user);
        UserResponseEntityDto Edit2(string userId, T user);
        UserResponseEntityDto Delete2(string id);
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
