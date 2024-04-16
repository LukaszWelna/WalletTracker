using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.ApplicationUser;
using WalletTracker.Application.Mappings;
using WalletTracker.Application.Services;

namespace WalletTracker.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services) 
        {
            // User context
            services.AddScoped<IUserContextService, UserContextService>();

            // Controller services
            services.AddScoped<IIncomeService, IncomeService>();

            // Add Auto mapper
            services.AddAutoMapper(typeof(IncomeMappingProfile));
        }
    }
}
