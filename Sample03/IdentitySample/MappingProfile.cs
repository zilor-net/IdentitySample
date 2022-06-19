using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentitySample.Entites;
using IdentitySample.Models;

namespace IdentitySample
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegistrationModel, User>()
                .ForMember(
                    user => user.UserName, 
                    expression => expression.MapFrom(userModel => userModel.Email));
        }
    }
}
