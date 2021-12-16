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

        #region Get All User
        [Route("users")]
        [HttpGet]
        public ActionResult GetAllUser()
        {
            UserResponseListEntityDto reponse = _userService.GetAllUser2();
          //  var reponse = _userService.GetAllUser();
            return Ok(reponse);
        }
        #endregion

        #region Get User By Id
        [Route("user/{id}")]
        [HttpGet]
        //[Authorize]
        public ActionResult GetUserById([FromRoute] string id)
        {
            UserResponseEntityDto reponse = _userService.GetById2(id);
            return Ok(reponse);
        }
        #endregion

        #region Sreach By Condition
        [Route("user")]
        [HttpGet]
        public ActionResult SearchByCondition([FromQuery] UserSearchRepestDto user)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            var result = _userService.SearchByCondition(user);
            return Ok(result);

            //UserResponseEntityDto reponse = _userService.GetById2(id);
            //return Ok(reponse);
        }
        #endregion

        #region Add User
        [Route("user")]
        [HttpPost]
        public ActionResult AddUser([FromBody] User user)
        {
            // UserResponseEntityDto reponse = _userService.Add2(user);
            var result = _userService.AddUser(user);
            return Ok(result);
        }
        #endregion

        #region Edit User
        [Route("user/{id}")]
        [HttpPut]
        public ActionResult EditUser([FromRoute] string id, [FromBody] User user)
        {
            UserResponseEntityDto reponse = _userService.GetById2(id);
            return Ok(reponse);
        }
        #endregion

        #region Delete User
        [Route("user/{id}")]
        [HttpDelete]
        public ActionResult DeleteUser(string id)
        {
            //UserResponseEntityDto reponse = _userService.GetById2(id);
            Guid userId = _userService.CheckFormatGuid(id);
            var reponse = _userService.DeleteUser(userId);
            return Ok(reponse);
        }
        #endregion

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
