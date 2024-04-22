using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Expense;
using WalletTracker.Application.Income;
using WalletTracker.Domain.Entities;

namespace WalletTracker.Application.Mappings
{
    public class ExpenseMappingProfile : Profile
    {
        public ExpenseMappingProfile()
        {
            CreateMap<ExpenseDto, Domain.Entities.Expense>();

            CreateMap<ExpenseCategoryAssignedToUser, ExpenseCategoryAssignedToUserDto>();

            CreateMap<PaymentMethodAssignedToUser, PaymentMethodAssignedToUserDto>();
        }
    }
}
