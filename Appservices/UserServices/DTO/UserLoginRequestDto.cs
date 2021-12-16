using EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppServices.UserServices.DTO
{
    public class UserLoginRequestDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public Roles Roles { get; set; }
    }
}
