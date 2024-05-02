using Microsoft.EntityFrameworkCore;
using WalletTracker.Application.ApplicationUser;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using WalletTracker.Infrastructure.Persistence;

namespace WalletTracker.Infrastructure.Repositories
{
    public class ExpenseCategoryRepository : IExpenseCategoryRepository
    {
        private readonly WalletTrackerDbContext _dbContext;
        private readonly IUserContextService _userContextService;

        public ExpenseCategoryRepository(WalletTrackerDbContext dbContext, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
        }

        public async Task<IEnumerable<ExpenseCategoryDefault>> GetDefaultCategories()
            => await _dbContext.ExpenseCategoriesDefault.ToListAsync();

        public async Task SeedDefaultCategoriesToUser(IEnumerable<ExpenseCategoryAssignedToUser> expenseCategoriesAssignedToUser)
        {
            _dbContext.ExpenseCategoriesAssignedToUsers.AddRange(expenseCategoriesAssignedToUser);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ExpenseCategoryAssignedToUser>> GetCategoriesAssignedToLoggedUser()
        {
            var userId = _userContextService.GetCurrentUser().Id;

            var categoriesAssignedToUser = await _dbContext.ExpenseCategoriesAssignedToUsers
                .Where(c => c.UserId == userId).ToListAsync();

            return categoriesAssignedToUser;
        }

        public async Task Commit()
            => await _dbContext.SaveChangesAsync();

        public async Task Create(ExpenseCategoryAssignedToUser category)
        {
            _dbContext.ExpenseCategoriesAssignedToUsers.Add(category);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var category = await _dbContext.ExpenseCategoriesAssignedToUsers
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                throw new InvalidOperationException("Expense category with specified id doesn't exist.");
            }

            _dbContext.ExpenseCategoriesAssignedToUsers.Remove(category);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<ExpenseCategoryAssignedToUser?> GetByName(string name)
        {
            var userId = _userContextService.GetCurrentUser().Id;

            var category = await _dbContext.ExpenseCategoriesAssignedToUsers
                .Where(c => c.UserId == userId)
                .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());

            return category;
        }

        public async Task<ExpenseCategoryAssignedToUser> GetById(int id)
        {
            var category = await _dbContext.ExpenseCategoriesAssignedToUsers
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                throw new InvalidOperationException("Expense category with specified id doesn't exist.");
            }

            return category;
        }
    }
}
