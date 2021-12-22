using EFCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.UserServices.DTO
{
    public class EditUserRepuestDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public Roles Role { get; set; }
    }
}
