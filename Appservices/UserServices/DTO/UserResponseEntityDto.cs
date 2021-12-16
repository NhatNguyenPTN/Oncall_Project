using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.UserServices.DTO
{
     public class UserResponseEntityDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }      
        public object Data { get; set; }
    }
}
