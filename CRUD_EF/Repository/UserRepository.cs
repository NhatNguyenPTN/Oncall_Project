using CRUD_EF.DbConnection;
using CRUD_EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CRUD_EF.Repository
{
    public class UserRepository : IUserRepository<User>
    {
        public List<User> GetAllUser()
        {
            using var userContext = new UserContext();
            var userList = userContext.users.ToList();

            return userList;
        }
        public User GetUserById(Guid id)
        {
            using var userContext = new UserContext();
            var user = userContext.users.Find(id);
            return user;
        }
        public bool AddUser(User user)
        {
            using var userContext = new UserContext();
            userContext.users.Add(user);

            int result = userContext.SaveChanges();
            if (result > 0)
            {
                return true;
            }
            else { return false; }
        }
        public bool DeleteUser(Guid id)
        {
            using var userContext = new UserContext();
            var user = (from u in userContext.users where (u.Id == id) select u).FirstOrDefault();
            userContext.users.Remove(user);
            int result = userContext.SaveChanges();

            if (result == 1)
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
            using var userContext = new UserContext();
            var userFind = (from u in userContext.users where (u.Id == userId) select u).FirstOrDefault();

            if (userFind != null)
            {
                userFind.Age = user.Age;
                userFind.FullName = user.FullName;
                userFind.Email = user.Email;
                               
                int result = userContext.SaveChanges();

                if (result > 0) { return true; } else return false;
            }
            else { return false; }


        }
        public bool IsExistUser(Guid id)
        {
            using var userContext = new UserContext();
            var user = (from u in userContext.users where (u.Id == id) select u).FirstOrDefault();
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
        public List<User> SearchByCondition(UserSearch user)
        {
            using var userContext = new UserContext();

            bool isExistEmail = user.Email != null;
            bool isExistFullName = user.FullName != null;
            bool isExistAge = user.Age > 0;

            var userList = userContext.users.ToList();

            List<User> userLisrSearch = new List<User>();

            //if (!isExistEmail && !isExistFullName && !isExistAge)
            //{
            //    return userList;
            //}

            //if (isExistEmail || isExistFullName || isExistAge)
            //{
            //    if (isExistEmail)
            //    {
            //        userLisrSearch = userContext.users
            //              .Where(userItem => userItem.Email.ToLower().Trim(' ').Contains(user.Email.ToLower().Trim(' ')))
            //              .ToList();
            //        userLisrSearch = new List<User>(userLisrSearch);
            //    }
            //    if (isExistAge)
            //    {
            //        userLisrSearch = userContext.users
            //              .Where(userItem => userItem.Age.Equals(user.Age))
            //              .ToList();
            //        userLisrSearch = new List<User>(userLisrSearch);
            //    }
            //    if (isExistFullName)
            //    {
            //        userLisrSearch = userContext.users
            //              .Where(userItem => userItem.FullName.ToLower().Trim(' ').Contains(user.FullName.ToLower().Trim(' ')))
            //              .ToList();
            //        userLisrSearch = new List<User>(userLisrSearch);
            //    }
            //    if (isExistAge && isExistEmail)
            //    {
            //        userLisrSearch = userContext.users
            //                .Where(userItem => userItem.Age.Equals(user.Age))
            //                .Where(userItem => userItem.Email.ToLower().Contains(user.Email.ToLower()))
            //                .ToList();
            //        userLisrSearch = new List<User>(userLisrSearch);
            //    }
            //    if (isExistAge && isExistFullName)
            //    {
            //        userLisrSearch = userContext.users
            //              .Where(userItem => userItem.FullName.ToLower().Trim(' ').Contains(user.FullName.ToLower().Trim(' ')))
            //              .Where(userItem => userItem.Age.Equals(user.Age))                          
            //              .ToList();
            //        userLisrSearch = new List<User>(userLisrSearch);
            //    }
            //    if (isExistEmail && isExistFullName)
            //    {
            //        userLisrSearch = userContext.users
            //              .Where(userItem => userItem.FullName.ToLower().Trim(' ').Contains(user.FullName.ToLower().Trim(' ')))
            //              .Where(userItem => userItem.Email.ToLower().Contains(user.Email.ToLower()))
            //              .ToList();
            //        userLisrSearch = new List<User>(userLisrSearch);
            //    }
            //    if (isExistEmail && isExistAge && isExistFullName)
            //    {
            //        userLisrSearch = userContext.users
            //              .Where(userItem => userItem.FullName.ToLower().Trim(' ').Contains(user.FullName.ToLower().Trim(' ')))
            //              .Where(userItem => userItem.Age.Equals(user.Age))
            //              .Where(userItem => userItem.Email.ToLower().Contains(user.Email.ToLower()))
            //              .ToList();
            //        userLisrSearch = new List<User>(userLisrSearch);
            //    }
            //}            

            var result = userList.Where(u => NotMatch(u, "FullName,Email,Age", user.FullName)).ToList();



            return result;
        }

        private static IEnumerable<PropertyInfo> GetProperties(object model)
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
            using var userContext = new UserContext();
            var user = (from u in userContext.users
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

            using var userContext = new UserContext();

            var user = (from u in userContext.users
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
