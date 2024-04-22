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
        }
    }
}