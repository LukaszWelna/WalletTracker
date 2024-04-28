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
    public class IncomeCategoryRepository : IIncomeCategoryRepository
    {
        private readonly WalletTrackerDbContext _dbContext;
        private readonly IUserContextService _userContextService;

        public IncomeCategoryRepository(WalletTrackerDbContext dbContext, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
        }

        public async Task<IEnumerable<IncomeCategoryDefault>> GetDefaultCategories()
            => await _dbContext.IncomeCategoriesDefault.ToListAsync();

        public async Task SeedDefaultCategoriesToUser(IEnumerable<IncomeCategoryAssignedToUser> incomeCategoriesAssignedToUser)
        {
            _dbContext.IncomeCategoriesAssignedToUsers.AddRange(incomeCategoriesAssignedToUser);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<IncomeCategoryAssignedToUser>> GetCategoriesAssignedToLoggedUser()
        {
            var userId = _userContextService.GetCurrentUser().Id;

            var categoriesAssignedToUser = await _dbContext
                .IncomeCategoriesAssignedToUsers
                .Where(c => c.UserId == userId).ToListAsync();

            return categoriesAssignedToUser;
        }

        public async Task Create(IncomeCategoryAssignedToUser category)
        {
            _dbContext.IncomeCategoriesAssignedToUsers.Add(category);

            await _dbContext.SaveChangesAsync();
        }

        public async Task Commit()
            => await _dbContext.SaveChangesAsync();

        public async Task DeleteById(int id)
        {
            var category = await _dbContext.IncomeCategoriesAssignedToUsers.FirstAsync(c => c.Id == id);

            _dbContext.IncomeCategoriesAssignedToUsers.Remove(category);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IncomeCategoryAssignedToUser?> GetByName(string name)
        {
            var userId = _userContextService.GetCurrentUser().Id;

            var category = await _dbContext.IncomeCategoriesAssignedToUsers
                .Where(c => c.UserId == userId)
                .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());

            return category;
        }

        public async Task<IncomeCategoryAssignedToUser> GetById(int id)
            => await _dbContext.IncomeCategoriesAssignedToUsers
            .FirstAsync(c => c.Id == id);
    }
}
