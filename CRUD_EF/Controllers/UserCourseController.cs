using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUD_EF.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserCourseController : ControllerBase
    {

        [Route("courses")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("hello");
        }


        [Route("course/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            return Ok("by id");
        }

        [Route("course")]
        [HttpPost]
        public IActionResult AddCourse([FromBody] string value)
        {
            return Ok(value);
        }

        [Route("course/{id}")]
        [HttpPut]
        public IActionResult EditCourse(int id)
        {
            return Ok("by id");
        }

        [Route("course/{id}")]
        [HttpDelete]
        public IActionResult DeleteCourse(int id)
        {
            return Ok("by id");
        }
    }
}
