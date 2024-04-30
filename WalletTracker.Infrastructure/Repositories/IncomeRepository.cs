using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.ApplicationUser;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using WalletTracker.Domain.Models;
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

        public async Task<IEnumerable<IEnumerable<Income>>> GetUserIncomesFromPeriod(DateOnly startDate, DateOnly endDate)
        {
            var userId = _userContextService.GetCurrentUser().Id;

            var incomes = await _dbContext.Incomes
                .AsNoTracking()
                .Include(i => i.Category)
                .Where(i => (i.UserId == userId) && (i.IncomeDate >= startDate && i.IncomeDate <= endDate))
                .GroupBy(i => i.IncomeDate)
                .OrderByDescending(g => g.Key)
                .Select(g => g.OrderByDescending(i => i.CreatedAt).ToList())
                .ToListAsync();

            return incomes;
        }

        public async Task DeleteIncomeById(int incomeId)
        {
            var income = await _dbContext.Incomes.FirstAsync(i => i.Id == incomeId);

            _dbContext.Incomes.Remove(income);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Income> GetIncomeById(int incomeId)
            => await _dbContext.Incomes.FirstAsync(i => i.Id == incomeId);

        public async Task Commit()
            => await _dbContext.SaveChangesAsync();

        public async Task<IEnumerable<IncomeTotalAmountInCategoryDto>> GetTotalAmountInCategories(DateOnly startDate, DateOnly endDate)
        {
            var userId = _userContextService.GetCurrentUser().Id;

            var totalAmountInCategories = await _dbContext.Incomes
                .Include(i => i.Category)
                .Where(i => i.UserId == userId && (i.IncomeDate >= startDate && i.IncomeDate <= endDate))
                .GroupBy(i => i.Category.Name)
                .Select(g => new IncomeTotalAmountInCategoryDto
                {
                    CategoryName = g.Key,
                    TotalAmount = g.Sum(c => c.Amount)
                })
                .ToListAsync();

            return totalAmountInCategories;
        }
    }
}
