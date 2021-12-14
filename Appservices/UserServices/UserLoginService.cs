using Appservices.UserServices.Interface;
using EFCore.DbConnection;
using EFCore.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Appservices.UserServices
{
    public class UserLoginService : IUserLoginService
    {
        private readonly UserContext _userContext;

        public UserLoginService(UserContext userContext)
        {
            _userContext = userContext;
        }


        public bool IsExistUser(string fullname)
        {
            //using var userContext = new UserContext();
            var user = _userContext.Users
                        .Where(u => (u.FullName.Trim(' ').ToLower() == fullname.Trim(' ').ToLower()))
                        .SingleOrDefault();
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsTrueEmail(UserLoginRequestDto user)
        {
            //  using var userContext = new UserContext();
            var userFind = _userContext.Users
                            .Where(u=> (u.FullName.Trim(' ').ToLower() == user.FullName.Trim(' ').ToLower() && u.Email == user.Email))
                            .FirstOrDefault();
            if (user == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<User> GetAllUser()
        {
            //   using var userContext = new UserContext();
            var userList = _userContext.Users.ToList();
            return userList;
        }

        public string GenerateToken(UserLoginRequestDto user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKey = "NguyenTruongNhat";
            var secretKyBytes = Encoding.UTF8.GetBytes(secretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("FullName", user.FullName)
                }),
                Expires = DateTime.UtcNow.AddSeconds(45),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKyBytes), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);

            return jwtTokenHandler.WriteToken(token);
            throw new NotImplementedException();

        }
    }
}
