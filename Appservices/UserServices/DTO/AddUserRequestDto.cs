using EFCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.UserServices.DTO
{
    public class AddUserRequestDto
    {        
        public string Email { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public Guid? CourseId { get; set; }        
        
    }
}
