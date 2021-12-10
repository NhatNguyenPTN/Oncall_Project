using CRUD_EF.DbConnection;
using CRUD_EF.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_EF.Repository
{
    public class UserLoginRepository : IUserLoginRepository
    {
        private readonly UserContext _userContext;

        public UserLoginRepository(UserContext userContext)
        {
            _userContext = userContext;
        }


        public bool IsExistUser(string fullname)
        {
            //using var userContext = new UserContext();
            var user = (from u in _userContext.Users
                        where (u.FullName.Trim(' ').ToLower() == fullname.Trim(' ').ToLower())
                        select u).SingleOrDefault();
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsTrueEmail(UserLogin user)
        {
          //  using var userContext = new UserContext();
            var userFind = (from u in _userContext.Users
                            where (u.FullName.Trim(' ').ToLower() == user.FullName.Trim(' ').ToLower() && u.Email == user.Email)
                            select u).FirstOrDefault();
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

        public string GenerateToken(UserLogin user)
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
