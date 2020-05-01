using AutoMapper;
using PWAppApi.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAppApi.Models.Dto
{
    public class BalanseProfile : Profile
    {
        public BalanseProfile()
        {
            CreateMap<Balance, BalanceDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User !=null ? src.User.FullUserName : string.Empty));
            CreateMap<BalanceDto, Balance>();
        }
    }
}
