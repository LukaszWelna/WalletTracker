using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using WalletTracker.Application.ApplicationUser;
using WalletTracker.Application.Income.Commands.CreateIncome;
using WalletTracker.Application.Mappings;

namespace WalletTracker.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            // Add User context
            services.AddScoped<IUserContextService, UserContextService>();

            // Add Auto mapper
            services.AddAutoMapper(typeof(IncomeMappingProfile));

            // Add MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateIncomeCommand)));

            // Add Fluent validation
            services.AddValidatorsFromAssemblyContaining<CreateIncomeCommandValidator>()
                    .AddFluentValidationAutoValidation()
                    .AddFluentValidationClientsideAdapters();
        }
    }
}
