using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Income;
using WalletTracker.Domain.Entities;

namespace WalletTracker.Application.Mappings
{
    public class IncomeMappingProfile : Profile
    {
        public IncomeMappingProfile()
        {
            CreateMap<IncomeDto, Domain.Entities.Income>();

            CreateMap<IncomeCategoryAssignedToUser, IncomeCategoryAssignedToUserDto>();

            CreateMap<List<IncomeCategoryAssignedToUser>, IncomeDto>()
                .ForMember(i => i.IncomeDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(DateTime.UtcNow)))
                .ForMember(i => i.UserCategoryDtos, opt => opt.MapFrom(src => src));
        }
    }
}