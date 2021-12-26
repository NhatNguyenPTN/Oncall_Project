using AppServices.UserServices.DTO;
using AutoMapper;
using EFCore.Model;

namespace AppServices.UserServices.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserLoginRequestDto>();
        }
    }
}
