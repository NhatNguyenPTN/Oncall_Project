using CRUD_EF.DbConnection;
using CRUD_EF.Model;
using CRUD_EF.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUD_EF.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // private IUserRepository<User> _userRepository;
        private readonly IUserRepository<User> _userRepository;
        public UserController(IUserRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }


        [Route("users")]
        [HttpGet]
        public ActionResult GetAllUser()
        {
            try
            {
                var userList = _userRepository.GetAllUser();
                if (_userRepository.IsUserListEmty(userList))
                {
                    return Ok("User List Is Emty ");
                }
                return Ok(userList);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("user/{id}")]
        [HttpGet]
        [Authorize]
        public ActionResult GetUserById([FromRoute] string id)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest(); }

                Guid userId = _userRepository.CheckFormatGuid(id);

                if (userId == Guid.Empty) { return BadRequest("Error Format"); }

                var user = _userRepository.GetUserById(userId);

                if (user != null) { return Ok(user); }
                else
                {
                    return BadRequest("User dose not exist");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("user")]
        [HttpGet]
        public ActionResult SearchByCondition([FromQuery] UserSearch user)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest(); }

                var result = _userRepository.SearchByCondition(user);
                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("user")]
        [HttpPost]
        public ActionResult AddUser([FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest("Binding false"); }

                bool isValidName = _userRepository.IsValidName(user.FullName);

                if (isValidName)
                {
                    var result = _userRepository.AddUser(user);
                    return Ok(result);
                }
                else { return BadRequest(); }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("user/{id}")]
        [HttpPut]
        public ActionResult EditUser([FromRoute] string id, [FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest(); }

                Guid userId = _userRepository.CheckFormatGuid(id);
                if (userId == Guid.Empty) { return BadRequest("Error Format"); }

                bool isValidName = _userRepository.IsValidNameEdit(userId, user.FullName);
                if (!isValidName) { return BadRequest("Name is exist"); }

                if (_userRepository.IsExistUser(userId))
                {
                    var result = _userRepository.EditUser(userId, user);
                    return Ok(result);
                }
                return BadRequest("User dose not exist");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("user/{id}")]
        [HttpDelete]
        public ActionResult DeleteUser(string id)
        {
            try
            {
                Guid userId = _userRepository.CheckFormatGuid(id);

                if (!ModelState.IsValid) { return BadRequest(); }

                if (userId == Guid.Empty) { return BadRequest("Error Format"); }

                if (_userRepository.IsExistUser(userId))
                {
                    var result = _userRepository.DeleteUser(userId);
                    return Ok(result);
                }
                return BadRequest("User dose not exist");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("user/test")]
        [HttpDelete]
        public ActionResult Test([FromQuery] User user)
        {
            return Ok();
        }
    }
}
