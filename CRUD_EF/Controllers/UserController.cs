using Appservices.UserServices.Interface;
using AppServices.UserServices.DTO;
using EFCore.Model;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUD_EF.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService<User> _userService;
        public UserController(IUserService<User> userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Function to get all user      
        /// </summary>
        /// This endpoint is used to get all user is exist on system 
        /// - GET: /api/users
        /// <returns>success, message, data</returns>
        [Route("users")]
        [HttpGet]
        public ActionResult GetAllUser()
        {
            UserResponseListEntityDto reponse = _userService.GetAllUser();
            return Ok(reponse);
        }


        /// <summary>
        /// Function to get user by userId
        /// </summary>
        /// This endpoint is used to get user by userId 
        /// - GET: /api/user/userId
        /// <param name="userId"></param>        
        /// <returns>success, message, data</returns>
        [Route("user/{userId}")]
        [HttpGet]
        public ActionResult GetUserById([FromRoute] string userId)
        {
            UserResponseEntityDto reponse = _userService.GetUserById(userId);
            return Ok(reponse);
        }

        /// <summary>
        /// Funtion to search user by fullname and email
        /// </summary>
        /// This endpoint is used to get user by userId 
        /// - GET: /api/user
        /// <param name="body"></param>
        /// <returns>success, message, data</returns>
        [Route("user")]
        [HttpGet]
        public ActionResult SearchByCondition([FromQuery] UserSearchRepestDto body)
        {
            UserResponseListEntityDto response = _userService.SearchByCondition(body);
            //var response = _userService.SearchByCondition(user);
            return Ok(response);
        }


        /// <summary>
        /// Function to add a new user 
        /// </summary>
        /// This endpoint is used to create user
        /// - POST: /api/user
        /// <param name="body"></param>
        /// <returns>success, message, data</returns>
        [Route("user")]
        [HttpPost]
        public ActionResult AddUser([FromBody] AddUserRequestDto body)
        {
            UserResponseEntityDto response = _userService.Add(body);
            return Ok(response);
        }


        /// <summary>
        /// Function to edit user by userId
        /// </summary>
        /// This endpoint is used to edit user by userId
        /// - PUT: /api/user/userId
        /// <param name="userId"></param>
        /// <param name="body"></param>
        /// <returns>success, message, data</returns>
        [Route("user/{id}")]
        [HttpPut]
        public ActionResult EditUser([FromRoute] string userId, [FromBody] EditUserRepuestDto body)
        {
            UserResponseEntityDto reponse = _userService.Edit(userId, body);
            return Ok(reponse);
        }

        /// <summary>
        /// Function to delete user by userId
        /// </summary>
        /// This endpoint is used to delete user by userId
        /// - DELETE: /api/user/userId
        /// <param name="userId"></param>
        /// <returns>success, message, data</returns>
        [Route("user/{id}")]
        [HttpDelete]
        public ActionResult DeleteUser(string id)
        {
            UserResponseEntityDto reponse = _userService.Delete(id);
            return Ok(reponse);
        }
        

        #region api error server
        [Route("error-server")]
        [HttpGet]
        public ActionResult TesstErrorServer()
        {
            int zero = 0;
            int result = 4 / zero;
            return Ok(result);
        }
        #endregion
    }
}
