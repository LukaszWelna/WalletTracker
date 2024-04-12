using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Entities;
using WalletTracker.Infrastructure.Persistence;

namespace WalletTracker.Infrastructure.Seeders
{
    // Seed default payment methods entity
    public class PaymentMethodsDefaultSeeder
    {
        private readonly WalletTrackerDbContext _dbContext;

        public PaymentMethodsDefaultSeeder(WalletTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                if (!_dbContext.PaymentMethodsDefault.Any())
                {
                    var paymentMethodDefault1 = new PaymentMethodDefault()
                    {
                        Name = "Cash"
                    };

                    var paymentMethodDefault2 = new PaymentMethodDefault()
                    {
                        Name = "Debit card"
                    };

                    var paymentMethodDefault3 = new PaymentMethodDefault()
                    {
                        Name = "Credit card"
                    };

                    _dbContext.PaymentMethodsDefault.AddRange(paymentMethodDefault1, paymentMethodDefault2, paymentMethodDefault3);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
