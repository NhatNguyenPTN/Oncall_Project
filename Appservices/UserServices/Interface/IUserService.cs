using AppServices.UserServices.DTO;
using System;

namespace Appservices.UserServices.Interface
{
    public interface IUserService
    {            
        UserResponseListEntityDto GetAll();
        UserResponseEntityDto GetrById(string userId);
        UserResponseListEntityDto SearchByCondition(UserSearchRepestDto condition);
        UserResponseEntityDto Add(AddUserRequestDto user);
        UserResponseEntityDto Edit(string userId, EditUserRepuestDto user);
        UserResponseEntityDto Delete(string id);              
        bool IsValidNameEdit(Guid id, string fullname);                  
        Guid CheckFormatGuid(string id);
    }
}
