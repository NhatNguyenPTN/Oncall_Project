using AutoMapper;
using EFCore.Model;
using EFCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.MapperConfig
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserLoginRequestDto>();           
        }
    }
}
