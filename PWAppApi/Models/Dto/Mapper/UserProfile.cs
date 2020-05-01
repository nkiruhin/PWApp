using AutoMapper;
using PWAppApi.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAppApi.Models.Dto
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateDto, User>()
                 .ForMember(dest => dest.FullUserName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<User, UserSelectListDto>();
        }
    }
}
