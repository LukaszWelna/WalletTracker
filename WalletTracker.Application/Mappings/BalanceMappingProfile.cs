using AutoMapper;
using WalletTracker.Application.Balance;
using WalletTracker.Domain.Models;

namespace WalletTracker.Application.Mappings
{
    public class BalanceMappingProfile : Profile
    {
        public BalanceMappingProfile()
        {
            CreateMap<ExpenseTotalAmountInCategoryDto, BalanceCanvasDto>()
                .ForMember(b => b.label, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(b => b.y, opt => opt.MapFrom(src => src.TotalAmount));

            CreateMap<ExpenseTotalAmountInCategoryDto, BalanceCanvasDto>()
                .ForMember(b => b.label, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(b => b.y, opt => opt.MapFrom(src => src.TotalAmount));
        }
    }
}
