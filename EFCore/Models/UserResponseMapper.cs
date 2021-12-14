using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Models
{
    public class UserResponseMapper
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
    }
}
