using AutoMapper;
using CRUD_EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_EF.MapperConfig
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<User, UserLogin>();
        }       
    }
}
