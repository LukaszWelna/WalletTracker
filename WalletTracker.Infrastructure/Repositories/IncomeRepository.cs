using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using WalletTracker.Infrastructure.Persistence;

namespace WalletTracker.Infrastructure.Repositories
{
    // Income repository - operations on WalletTrackerDbContext
    public class IncomeRepository : IIncomeRepository
    {
        private readonly WalletTrackerDbContext _dbContext;

        public IncomeRepository(WalletTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Create(Income income)
        {
            _dbContext.Incomes.Add(income);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<IncomeCategoryDefault>> GetDefaultCategories()
            => await _dbContext.IncomeCategoriesDefault.ToListAsync();

        public async Task SeedDefaultCategoriesToUser(List<IncomeCategoryAssignedToUser> incomeCategoriesAssignedToUser)
        {
            _dbContext.IncomeCategoriesAssignedToUsers.AddRange(incomeCategoriesAssignedToUser);
            await _dbContext.SaveChangesAsync();
        }
    }
}
