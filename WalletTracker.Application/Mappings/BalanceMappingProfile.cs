using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Balance;
using WalletTracker.Application.Expense;
using WalletTracker.Domain.Models;

namespace WalletTracker.Application.Mappings
{
    public class BalanceMappingProfile : Profile
    {
        public BalanceMappingProfile()
        {
            CreateMap<ExpenseTotalAmountInCategoryDto, BalanceDto>()
                .ForMember(b => b.label, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(b => b.y, opt => opt.MapFrom(src => src.TotalAmount));
        }
    }
}
