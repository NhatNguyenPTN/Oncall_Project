using EFCore.Models;

namespace AppServices.UserServices.DTO
{
    public class UserLoginRequestDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public Roles Roles { get; set; }
    }
}
