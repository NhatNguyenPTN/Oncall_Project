using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Models
{
    public class UserResponseDto
    {
        public bool Success { get; set; }
        public string? Message { get; set; }        
        public IEnumerable? Data { get; set; }
    }
}
