using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AppServices.UserServices.DTO
{
    public class UserResponseListEntityDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }        
        public IEnumerable Data { get; set; }
       
    }
}
