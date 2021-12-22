using Appservices.UserServices.Interface;
using AppServices.UserServices.DTO;
using EFCore.DbConnection;
using EFCore.Model;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Appservices.UserServices
{
    public class UserService : IUserService<User>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(UserContext userContext, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        /// <summary>
        /// Check GUID format
        /// </summary>
        /// <param name="id"></param>
        /// <returns>userID</returns>
        public Guid CheckFormatGuid(string id)
        {
            Guid userId = Guid.Empty;
            bool isValid = Guid.TryParse(id, out userId);
            if (isValid)
            {
                return userId;
            }
            return userId;
        }

        /// <summary>
        /// Check fullname valid
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fullname"></param>
        /// <returns></returns>
        public bool IsValidNameEdit(Guid id, string fullname)
        {

            var user = _unitOfWork.UserRepository.GetAll()
                        .Where(u => (u.FullName.Trim(' ').ToLower() == fullname.Trim(' ').ToLower()))
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

        /// <summary>
        /// Get all user
        /// </summary>
        /// <returns></returns>
        public UserResponseListEntityDto GetAllUser()
        {
            UserResponseListEntityDto response = new UserResponseListEntityDto();

            var userList = _unitOfWork.UserRepository.GetAll();

            if (userList.Count() == 0)
            {
                response.Success = true;
                response.Message = "User List Is Emty";
                response.Data = userList;
                return response;
            }

            response.Success = true;
            response.Message = "Get Successfully";
            response.Data = userList;

            return response;
        }

        public UserResponseEntityDto GetUserById(string userId)
        {
            UserResponseEntityDto response = new UserResponseEntityDto();

            if (!string.IsNullOrEmpty(userId))
            {
                Console.WriteLine(true);
            }
            else { Console.WriteLine(false); }

            Guid currentUserId = CheckFormatGuid(userId);
            //Check guid format
            if (currentUserId == Guid.Empty)
            {
                response.Success = false;
                response.Message = "Error Format";
                return response;
            }
            //Get user by id
            var currentUser = _unitOfWork.UserRepository.GetById(currentUserId);
            if (currentUser == null)
            {
                response.Success = true;
                response.Message = "User dose not exist";
                return response;
            }
            //Return successfully
            response.Success = true;
            response.Message = "Get Successfully";
            response.Data = currentUser;

            return response;
        }

        public UserResponseListEntityDto SearchByCondition(UserSearchRepestDto userSearch)
        {
            UserResponseListEntityDto response = new UserResponseListEntityDto();
            var userList = _unitOfWork.UserRepository.GetAll()
                .Where(user => NotMatch(user, "Email", userSearch.Email))
                .Where(user => NotMatch(user, "FullName", userSearch.FullName)).ToList();

            if (userList.Count() == 0)
            {
                response.Success = true;
                response.Message = "User List is Emty";
                response.Data = userList;
                return response;
            }
            response.Success = true;
            response.Message = "Get Successfully";
            response.Data = userList;

            return response;
        }

        public UserResponseEntityDto Add(AddUserRequestDto userRequestDto)
        {
            UserResponseEntityDto response = new UserResponseEntityDto();

            //can use mapper
            User user = new User();

            user.Email = userRequestDto.Email;
            user.FullName = userRequestDto.FullName;
            user.Age = userRequestDto.Age;
            if (userRequestDto.Role == 0)
            {
                user.Role = 0;
            }
            else
            {
                user.Role = userRequestDto.Role;
            }
            //Add user
            _unitOfWork.UserRepository.Add(user);
            //Save changes
            var result = _unitOfWork.SaveChanges();
            if (result == 0)
            {
                response.Success = false;
                response.Message = "Add false";
            }
            //Return successfully
            response.Success = true;
            response.Message = "Add Successfully";
            response.Data = user;

            return response;
        }

        public UserResponseEntityDto Edit(string userId, EditUserRepuestDto user)
        {
            UserResponseEntityDto response = new UserResponseEntityDto();
            //Check guid format
            Guid currentUserId = CheckFormatGuid(userId);
            if (currentUserId == Guid.Empty)
            {
                response.Success = true;
                response.Message = "Error Format";
                return response;
            }
            //Get user by id
            var currentUser = _unitOfWork.UserRepository.GetById(currentUserId);
            if (currentUser == null)
            {
                response.Success = true;
                response.Message = "User dose not exist";
                return response;
            }
            //Check Valid FullName
            if (!IsValidNameEdit(currentUserId, user.FullName))
            {
                response.Success = false;
                response.Message = "Full Name Is Exist";
                return response;
            }
            //Edit User
            currentUser.Email = user.Email;
            currentUser.FullName = user.FullName;
            currentUser.Age = user.Age;
            currentUser.Role = user.Role;
            _unitOfWork.UserRepository.Edit(currentUser);
            //Save changes
            var result = _unitOfWork.SaveChanges();
            if (result == 0)
            {
                response.Success = false;
                response.Message = "Edit false";
            }
            //Return successfully
            response.Success = true;
            response.Message = "Edit Successfully";
            response.Data = currentUser;

            return response;
        }

        public UserResponseEntityDto Delete(string userId)
        {
            UserResponseEntityDto response = new UserResponseEntityDto();

            Guid currentUserId = CheckFormatGuid(userId);
            //Check guid format
            if (currentUserId == Guid.Empty)
            {
                response.Success = true;
                response.Message = "Error Format";
                return response;
            }
            //Get user by id
            var currentUser = _unitOfWork.UserRepository.GetById(currentUserId);
            if (currentUser == null)
            {
                response.Success = true;
                response.Message = "User dose not exist";
                return response;
            }
            //Delete User
            _unitOfWork.UserRepository.Delete(currentUser);
            //Save changes
            var result = _unitOfWork.SaveChanges();
            if (result == 0)
            {
                response.Success = false;
                response.Message = "Delete false";
            }
            //Return successfully
            response.Success = true;
            response.Message = "Delete Successfully";

            return response;
        }
    }
}
