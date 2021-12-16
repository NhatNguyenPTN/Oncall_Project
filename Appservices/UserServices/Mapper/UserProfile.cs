using AppServices.UserServices.DTO;
using AutoMapper;
using EFCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

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
