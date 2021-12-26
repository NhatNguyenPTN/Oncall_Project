using AppServices.UserServices.DTO;
using EFCore.Model;
using System.Collections.Generic;

namespace Appservices.UserServices.Interface
{
    interface IUserLoginService
    {
        public User IsExistUser(string fullname);
        public bool IsTrueEmail(UserLoginRequestDto user);
        List<User> GetAllUser();
        public string GenerateToken(UserLoginRequestDto user);
    }
}
