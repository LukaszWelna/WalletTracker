using AutoMapper;
using WalletTracker.Application.Expense;
using WalletTracker.Application.Expense.Commands.EditExpenseById;
using WalletTracker.Domain.Entities;

namespace WalletTracker.Application.Mappings
{
    public class ExpenseMappingProfile : Profile
    {
        public ExpenseMappingProfile()
        {
            CreateMap<CreateExpenseDto, Domain.Entities.Expense>();

            CreateMap<ExpenseCategoryAssignedToUser, ExpenseCategoryAssignedToUserDto>();

            CreateMap<PaymentMethodAssignedToUser, PaymentMethodAssignedToUserDto>();

            CreateMap<Domain.Entities.Expense, GetExpenseDto>()
                .ForMember(dto => dto.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dto => dto.PaymentName, opt => opt.MapFrom(src => src.Payment.Name));

            CreateMap<Domain.Entities.Expense, EditExpenseByIdCommand>();

            CreateMap<ExpenseCategoryAssignedToUser, CategoryLimitDto>();
        }
    }
}
