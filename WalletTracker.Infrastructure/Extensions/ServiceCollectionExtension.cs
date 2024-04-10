using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Infrastructure.Persistence;

namespace WalletTracker.Infrastructure.Extensions
{
    // Service collection extension method
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WalletTrackerDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("WalletTracker")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<WalletTrackerDbContext>();
        }


    }
}
