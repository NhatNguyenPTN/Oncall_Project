using AutoMapper;
using CRUD_EF.Model;
using CRUD_EF.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IUserLoginRepository _userLoginRepository;
        private readonly IMapper _mapper;

        public AuthenJWTController(IUserLoginRepository userLoginRepository, IMapper mapper)
        {
            _userLoginRepository = userLoginRepository;
            _mapper = mapper;
        }

        [Route("auth")]
        [HttpPost]
        public ActionResult Post([FromBody] UserLogin user)
        {
            try
            {
                var isExitUser = _userLoginRepository.IsExistUser(user.FullName);
                
                if (isExitUser)
                {
                    var token = _userLoginRepository.GenerateToken(user);
                    return Ok(token);
                }
                else
                {
                    return NotFound("User dose not exist");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

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
    }
}
