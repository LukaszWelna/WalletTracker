﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Models;
using WalletTracker.Infrastructure.Persistence;
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

            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<WalletTrackerDbContext>();

            services.AddScoped<IncomeCategoriesDefaultSeeder>();
            services.AddScoped<ExpenseCategoriesDefaultSeeder>();
            services.AddScoped<PaymentMethodsDefaultSeeder>();
        }
    }
}
