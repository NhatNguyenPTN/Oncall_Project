using CRUD_EF.DbConnection;
using CRUD_EF.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CRUD_EF.Repository
{
    public class UserRepository : IUserRepository<User>
    {
        private readonly UserContext _userContext;

        public UserRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public List<User> GetAllUser()
        {
            //sing var userContext = new UserContext();
            var userList = _userContext.Users.ToList();

            return userList;
        }
        public User GetUserById(Guid id)
        {
            //using var userContext = new UserContext();
            var user = _userContext.Users.Find(id);
            return user;
        }
        public bool AddUser(User user)
        {
            //   using var userContext = new UserContext();
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
            //   using var userContext = new UserContext();
            var user = (from u in _userContext.Users where (u.Id == id) select u).FirstOrDefault();
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
            //   using var userContext = new UserContext();
            var userFind = (from u in _userContext.Users where (u.Id == userId) select u).FirstOrDefault();

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
            //   using var userContext = new UserContext();
            var user = (from u in _userContext.Users where (u.Id == id) select u).FirstOrDefault();
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
        public List<User> SearchByCondition(UserSearch userSearch)
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
            var user = (from u in _userContext.Users
                        where (u.FullName.Trim(' ').ToLower() == fullname.Trim(' ').ToLower())
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

            //  using var userContext = new UserContext();

            var user = (from u in _userContext.Users
                        where (u.FullName.Trim(' ').ToLower() == fullname.Trim(' ').ToLower())
                        select u).FirstOrDefault();
            if (user == null || user.Id.Equals(id))
            {
                return true;
            }
            else
            {
                return false;
            };


        }
    }
}
