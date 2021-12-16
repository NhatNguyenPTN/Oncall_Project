using System;

namespace AppServices.UserServices.DTO
{
    public class UserResponseMapper
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
    }
}
