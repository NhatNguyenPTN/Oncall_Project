using System.Collections;

namespace AppServices.UserServices.DTO
{
    public class UserResponseListEntityDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }        
        public IEnumerable Data { get; set; }       
    }
}
