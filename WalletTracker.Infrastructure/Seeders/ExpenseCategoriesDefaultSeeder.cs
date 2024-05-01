using WalletTracker.Domain.Entities;
using WalletTracker.Infrastructure.Persistence;

namespace WalletTracker.Infrastructure.Seeders
{
    // Seed default expense categories entity
    public class ExpenseCategoriesDefaultSeeder
    {
        private readonly WalletTrackerDbContext _dbContext;

        public ExpenseCategoriesDefaultSeeder(WalletTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                if (!_dbContext.ExpenseCategoriesDefault.Any())
                {
                    var expenseCategoryDefault1 = new ExpenseCategoryDefault()
                    {
                        Name = "Transport"
                    };

                    var expenseCategoryDefault2 = new ExpenseCategoryDefault()
                    {
                        Name = "Food"
                    };

                    var expenseCategoryDefault3 = new ExpenseCategoryDefault()
                    {
                        Name = "Recreation"
                    };

                    var expenseCategoryDefault4 = new ExpenseCategoryDefault()
                    {
                        Name = "Health"
                    };

                    var expenseCategoryDefault5 = new ExpenseCategoryDefault()
                    {
                        Name = "Another"
                    };

                    _dbContext.ExpenseCategoriesDefault.AddRange(expenseCategoryDefault1, expenseCategoryDefault2, expenseCategoryDefault3,
                        expenseCategoryDefault4, expenseCategoryDefault5);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
