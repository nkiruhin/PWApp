using AutoMapper;
using PWAppApi.Models.Entity;
using System;

namespace PWAppApi.Models.Dto
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionListDto>()
               .ForMember(dest => dest.Sender, opt => opt.MapFrom(src => src.Sender.FullUserName))
               .ForMember(dest => dest.Recipient, opt => opt.MapFrom(src => src.Recipient.FullUserName));
            CreateMap<TransactionCreateDto, Transaction>()
              .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<Transaction, TransactionFormDto>()
              .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Recipient.Email))
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Recipient.Id));

        }    
    }
}
