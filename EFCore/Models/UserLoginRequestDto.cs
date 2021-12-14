using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Model
{
    public class UserLoginRequestDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}
