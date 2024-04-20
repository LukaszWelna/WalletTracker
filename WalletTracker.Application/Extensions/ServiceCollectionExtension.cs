using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.ApplicationUser;
using WalletTracker.Application.Income;
using WalletTracker.Application.Income.Commands.CreateIncome;
using WalletTracker.Application.Mappings;

namespace WalletTracker.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services) 
        {
            // User context
            services.AddScoped<IUserContextService, UserContextService>();

            // Add Auto mapper
            services.AddAutoMapper(typeof(IncomeMappingProfile));

            // Add MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateIncomeCommand)));

            // Fluent validation
            services.AddValidatorsFromAssemblyContaining<CreateIncomeCommandValidator>()
                    .AddFluentValidationAutoValidation()
                    .AddFluentValidationClientsideAdapters();

        }
    }
}
