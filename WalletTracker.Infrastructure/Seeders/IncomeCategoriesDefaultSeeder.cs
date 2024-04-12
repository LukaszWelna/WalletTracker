using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Entities;
using WalletTracker.Infrastructure.Persistence;

namespace WalletTracker.Infrastructure.Seeders
{
    // Seed default income categories entity
    public class IncomeCategoriesDefaultSeeder
    {
        private readonly WalletTrackerDbContext _dbContext;

        public IncomeCategoriesDefaultSeeder(WalletTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                if (!_dbContext.IncomeCategoriesDefault.Any())
                {
                    var incomeCategoryDefault1 = new IncomeCategoryDefault()
                    {
                        Name = "Salary"
                    };

                    var incomeCategoryDefault2 = new IncomeCategoryDefault()
                    {
                        Name = "Interest"
                    };

                    var incomeCategoryDefault3 = new IncomeCategoryDefault()
                    {
                        Name = "Allegro"
                    };

                    var incomeCategoryDefault4 = new IncomeCategoryDefault()
                    {
                        Name = "Another"
                    };

                    _dbContext.IncomeCategoriesDefault.AddRange(incomeCategoryDefault1, incomeCategoryDefault2, incomeCategoryDefault3, incomeCategoryDefault4);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
