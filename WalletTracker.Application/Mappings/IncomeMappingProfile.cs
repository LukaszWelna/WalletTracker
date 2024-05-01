using AutoMapper;
using WalletTracker.Application.Income;
using WalletTracker.Application.Income.Commands.EditIncomeById;
using WalletTracker.Domain.Entities;

namespace WalletTracker.Application.Mappings
{
    public class IncomeMappingProfile : Profile
    {
        public IncomeMappingProfile()
        {
            CreateMap<CreateIncomeDto, Domain.Entities.Income>();

            CreateMap<IncomeCategoryAssignedToUser, IncomeCategoryAssignedToUserDto>();

            CreateMap<Domain.Entities.Income, GetIncomeDto>()
                .ForMember(dto => dto.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Domain.Entities.Income, EditIncomeByIdCommand>();
        }
    }
}