using Appservices.UserServices;
using AppServices.UserServices.DTO;
using AutoMapper;

using EFCore.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUD_EF.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthenJWTController : ControllerBase
    {
        private readonly UserLoginService _userLoginRepository;
        

        public AuthenJWTController(UserLoginService userLoginRepository, IMapper mapper)
        {
            _userLoginRepository = userLoginRepository;
           
        }

        [Route("auth")]
        [HttpPost]
        public ActionResult Post([FromBody] UserLoginRequestDto user)
        {

            var currentUser = _userLoginRepository.IsExistUser(user.FullName);

            if (currentUser != null)
            {
                user.Roles = currentUser.Role;
                var token = _userLoginRepository.GenerateToken(user);
                return Ok(token);
            }
            else
            {
                return NotFound("User dose not exist");
            }
        }

        //user role
        [Route("role-user")]
        [HttpGet]
        [Authorize(Roles = "User")]
        public ActionResult RoleUser()
        {
            return Ok(" User Role");
        }

        //admin role
        [Route("role-admin")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult RoleAdmin()
        {
            return Ok(" Admin Role");
        }
        //user - admin role
        [Route("role-admin-user")]
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public ActionResult RoleAdminUser()
        {
            return Ok(" Admin User Role");
        }


        #region Api test http client
        [Route("test-http-client")]
        [HttpGet]
        public async Task<ActionResult> TestHttpClientAsync()
        {
            string result = "";
            var ping = new Ping();
            var pingreply = ping.Send("127.0.0.1");
            try
            {
                if (pingreply.Status == IPStatus.Success)
                {
                    using var httpClient = new HttpClient();
                    var httpMessageRequest = new HttpRequestMessage();

                    httpMessageRequest.Method = HttpMethod.Get;
                    httpMessageRequest.RequestUri = new Uri("http://localhost:5008/api/data");

                    var httpResponseMessage = await httpClient.SendAsync(httpMessageRequest);
                    var resultGet = await httpResponseMessage.Content.ReadAsStringAsync();
                    result = resultGet;
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok(result);
        }
        #endregion

    }
}
