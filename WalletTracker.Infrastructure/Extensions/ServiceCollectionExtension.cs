using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WalletTracker.Domain.Interfaces;
using WalletTracker.Domain.Models;
using WalletTracker.Infrastructure.Persistence;
using WalletTracker.Infrastructure.Repositories;
using WalletTracker.Infrastructure.Seeders;

namespace WalletTracker.Infrastructure.Extensions
{
    // Service collection extension method
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WalletTrackerDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("WalletTracker")));

            // Identity configuration
            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<WalletTrackerDbContext>();

            // Seeders
            services.AddScoped<IncomeCategoriesDefaultSeeder>();
            services.AddScoped<ExpenseCategoriesDefaultSeeder>();
            services.AddScoped<PaymentMethodsDefaultSeeder>();

            // Add repositories
            services.AddScoped<IIncomeRepository, IncomeRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IIncomeCategoryRepository, IncomeCategoryRepository>();
            services.AddScoped<IExpenseCategoryRepository, ExpenseCategoryRepository>();
            services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
        }
    }
}
