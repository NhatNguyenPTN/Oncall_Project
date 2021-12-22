using AppServices.UserServices.DTO;
using EFCore.Model;
using System;
using System.Collections.Generic;

namespace Appservices.UserServices.Interface
{
    public interface IUserService<T>
    {            
        UserResponseListEntityDto GetAllUser();
        UserResponseEntityDto GetUserById(string userId);
        UserResponseListEntityDto SearchByCondition(UserSearchRepestDto condition);
        UserResponseEntityDto Add(AddUserRequestDto user);
        UserResponseEntityDto Edit(string userId, EditUserRepuestDto user);
        UserResponseEntityDto Delete(string id);              
        bool IsValidNameEdit(Guid id, string fullname);                  
        Guid CheckFormatGuid(string id);
    }
}
