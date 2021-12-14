using AppRepositories;
using Appservices.UserServices.Interface;
using AutoMapper;
using EFCore.DbConnection;
using EFCore.Model;
using EFCore.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Appservices.UserServices
{
    public class UserService : IUserService<User>
    {
        private readonly UserContext _userContext;
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;       
        public UserService(UserContext userContext, IMapper mapper, UnitOfWork unitOfWork)
        {
            _userContext = userContext;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            

        }

        public List<User> GetAllUser()
        {
            var userList = _userContext.Users.ToList();          

            return userList;
        }
        public User GetUserById(Guid id)
        {        
            var user = _userContext.Users.Find(id);
            return user;
        }
        public bool AddUser(User user)
        {
            _userContext.Users.Add(user);

            int result = _userContext.SaveChanges();
            if (result > 0)
            {
                return true;
            }
            else { return false; }
        }
        public bool DeleteUser(Guid id)
        {
            var user = _userContext.Users.Find(id);

            _userContext.Users.Remove(user);

            int result = _userContext.SaveChanges();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool EditUser(Guid userId, User user)
        {
            var userFind = _userContext.Users.Find(userId);

            if (userFind != null)
            {
                userFind.Age = user.Age;
                userFind.FullName = user.FullName;
                userFind.Email = user.Email;

                int result = _userContext.SaveChanges();

                if (result > 0) { return true; } else return false;
            }
            else { return false; }
        }
        public bool IsExistUser(Guid id)
        {

            var user = _userContext.Users.Where(u => (u.Id == id)).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool IsUserListEmty(List<User> userList)
        {
            int numberUser = userList.Count();

            if (numberUser != 0)
            {
                return false;
            }
            return true;
        }
        public List<User> SearchByCondition(UserSearchRepestDto userSearch)
        {
            var userList = _userContext.Users.ToList();

            var result = userList
                .Where(user => NotMatch(user, "Email", userSearch.Email))
                .Where(user => NotMatch(user, "FullName", userSearch.FullName))
                .ToList();

            return result;
        }

        public static IEnumerable<PropertyInfo> GetProperties(object model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            return model.GetType().GetProperties().AsEnumerable();
        }

        /// <summary>
        /// Not match use for filter with list API
        /// </summary>
        /// <param name="model"></param>
        /// <param name="keys"></param>
        /// <param name="keySearch"></param>
        /// <returns>bool</returns>
        public static bool NotMatch(object model, string keys, string keySearch)
        {
            // Check if keys is empty > by pass
            if (string.IsNullOrEmpty(keys))
            {
                return true;
            }
            // Split list key from string
            string[] listKey = keys.Split(",");
            // Check if search is null > by pass
            if (null == keySearch || keySearch.Length == 0) return true;
            // Get List Properties of current Object
            IEnumerable<PropertyInfo> properties = GetProperties(model);
            // Loop search
            foreach (string key in listKey)
            {
                // Get Property
                PropertyInfo property = properties.FirstOrDefault(x => x.Name.ToLower().Equals(key.ToLower()));
                if (property != null)
                {
                    // Get value of property
                    string value = property.GetValue(model).ToString();
                    // Check contains
                    if (value?.ToLower().Contains(keySearch.ToLower()) == true) return true;
                }
            }
            return false;
        }
        public bool IsValidName(string fullname)
        {
            var user = _userContext.Users
                        .Where(u => (u.FullName.Trim(' ').ToLower() == fullname.Trim(' ').ToLower()))
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
        public Guid CheckFormatGuid(string id)
        {
            Guid userId;
            bool isValid = Guid.TryParse(id, out userId);
            if (isValid)
            {
                return userId;
            }
            else { return Guid.Empty; }

        }
        public bool IsValidNameEdit(Guid id, string fullname)
        {            

            var user = _userContext.Users
                        .Where(u => (u.FullName.Trim(' ').ToLower() == fullname.Trim(' ').ToLower())                        )
                        .FirstOrDefault();
            if (user == null || user.Id.Equals(id))
            {
                return true;
            }
            else
            {
                return false;
            };


        }

        public UserResponseDto GetAllUser2()
        {
            UserResponseDto response = new UserResponseDto();
            var userList = _unitOfWork.UserRepository.GetAll();
            if (userList.Count() == 0)
            {
                response.Success = true;
                response.Message = "User List Emty";
                response.Data = userList;
                return response;
            }
            //_logger.LogInformation("Get Successful");
           // var mm = _mapper.Map<List<UserResponseMapper>>(userList);

            response.Success = true;
            response.Message = "Get Successfully";
            response.Data = userList;

            return response; 
        }
    }
}
