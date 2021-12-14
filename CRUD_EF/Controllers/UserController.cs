

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Appservices;
using EFCore.Model;
using Appservices.UserServices.Interface;
using Microsoft.Extensions.Logging;
using EFCore.Models;
using AppRepositories;

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

        [Route("users")]
        [HttpGet]
        public ActionResult GetAllUser()
        {
            UserResponseDto reponse = _userService.GetAllUser2();                        
            return Ok(reponse);
        }

        [Route("user/{id}")]
        [HttpGet]
        [Authorize]
        public ActionResult GetUserById([FromRoute] string id)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            Guid userId = _userService.CheckFormatGuid(id);

            if (userId == Guid.Empty) { return BadRequest("Error Format"); }

            var user = _userService.GetUserById(userId);

            if (user != null) { return Ok(user); }
            else
            {
                return BadRequest("User dose not exist");
            }
        }

        [Route("user")]
        [HttpGet]
        public ActionResult SearchByCondition([FromQuery] UserSearchRepestDto user)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            var result = _userService.SearchByCondition(user);
            return Ok(result);
        }

        [Route("user")]
        [HttpPost]
        public ActionResult AddUser([FromBody] User user)
        {

            if (!ModelState.IsValid) { return BadRequest("Binding false"); }

            bool isValidName = _userService.IsValidName(user.FullName);

            if (isValidName)
            {
                var result = _userService.AddUser(user);
                return Ok(result);
            }
            else { return BadRequest("FullName is exist"); }
        }

        [Route("user/{id}")]
        [HttpPut]
        public ActionResult EditUser([FromRoute] string id, [FromBody] User user)
        {

            if (!ModelState.IsValid) { return BadRequest(); }

            Guid userId = _userService.CheckFormatGuid(id);
            if (userId == Guid.Empty) { return BadRequest("Error Format"); }

            bool isValidName = _userService.IsValidNameEdit(userId, user.FullName);
            if (!isValidName) { return BadRequest("Name is exist"); }

            if (_userService.IsExistUser(userId))
            {
                var result = _userService.EditUser(userId, user);
                return Ok(result);
            }
            return BadRequest("User dose not exist");
        }

        [Route("user/{id}")]
        [HttpDelete]
        public ActionResult DeleteUser(string id)
        {
            Guid userId = _userService.CheckFormatGuid(id);

            if (!ModelState.IsValid) { return BadRequest(); }

            if (userId == Guid.Empty) { return BadRequest("Error Format"); }

            if (_userService.IsExistUser(userId))
            {
                var result = _userService.DeleteUser(userId);
                return Ok(result);
            }
            return BadRequest("User dose not exist");
        }
        [Route("error-server")]
        [HttpGet]
        public ActionResult TesstErrorServer()
        {
            int zero = 0;
            int result = 4 / zero;
            return Ok(result);
        }
    }
}
