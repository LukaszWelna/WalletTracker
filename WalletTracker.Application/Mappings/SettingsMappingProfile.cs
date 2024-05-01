using AutoMapper;
using WalletTracker.Application.Settings;
using WalletTracker.Application.Settings.Commands.CreateExpenseCategory;
using WalletTracker.Application.Settings.Commands.CreateIncomeCategory;
using WalletTracker.Application.Settings.Commands.CreatePaymentMethod;
using WalletTracker.Domain.Entities;

namespace WalletTracker.Application.Mappings
{
    public class SettingsMappingProfile : Profile
    {
        public SettingsMappingProfile()
        {
            CreateMap<CreateIncomeCategoryCommand, IncomeCategoryAssignedToUser>();

            CreateMap<CreateExpenseCategoryCommand, ExpenseCategoryAssignedToUser>();

            CreateMap<ExpenseCategoryAssignedToUser, ExpenseCategorySettingsDto>();

            CreateMap<CreatePaymentMethodCommand, PaymentMethodAssignedToUser>();
        }
    }
}
