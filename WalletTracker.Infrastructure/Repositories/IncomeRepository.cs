using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.ApplicationUser;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using WalletTracker.Infrastructure.Persistence;

namespace WalletTracker.Infrastructure.Repositories
{
    // Income repository - operations on WalletTrackerDbContext
    public class IncomeRepository : IIncomeRepository
    {
        private readonly WalletTrackerDbContext _dbContext;
        private readonly IUserContextService _userContextService;

        public IncomeRepository(WalletTrackerDbContext dbContext, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
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
        public async Task<List<IncomeCategoryAssignedToUser>> GetCategoriesAssignedToLoggedUser()
        {
            var userId = _userContextService.GetCurrentUser().Id;

            var categoriesAssignedToUser = await _dbContext
                .IncomeCategoriesAssignedToUsers
                .Where(c => c.UserId == userId).ToListAsync();

            return categoriesAssignedToUser;
        }

    }
}
