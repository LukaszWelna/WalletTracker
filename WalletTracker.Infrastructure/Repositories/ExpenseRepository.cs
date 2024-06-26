﻿using Microsoft.EntityFrameworkCore;
using WalletTracker.Application.ApplicationUser;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;
using WalletTracker.Domain.Models;
using WalletTracker.Infrastructure.Persistence;

namespace WalletTracker.Infrastructure.Repositories
{
    // Expense repository - operations on WalletTrackerDbContext
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly WalletTrackerDbContext _dbContext;
        private readonly IUserContextService _userContextService;

        public ExpenseRepository(WalletTrackerDbContext dbContext, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
        }

        public async Task Create(Expense expense)
        {
            _dbContext.Expenses.Add(expense);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<IEnumerable<Expense>>> GetUserExpensesFromPeriod(DateOnly startDate, DateOnly endDate)
        {
            var userId = _userContextService.GetCurrentUser().Id;

            var expenses = await _dbContext.Expenses
                .AsNoTracking()
                .Include(e => e.Category)
                .Include(e => e.Payment)
                .Where(e => e.UserId == userId && (e.ExpenseDate >= startDate && e.ExpenseDate <= endDate))
                .GroupBy(e => e.ExpenseDate)
                .OrderByDescending(g => g.Key)
                .Select(g => g.OrderByDescending(e => e.CreatedAt).ToList())
                .ToListAsync();

            return expenses;
        }

        public async Task DeleteExpenseById(int expenseId)
        {
            var expense = await _dbContext.Expenses
                .FirstOrDefaultAsync(i => i.Id == expenseId);

            if (expense == null)
            {
                throw new InvalidOperationException("Expense with specified id doesn't exist.");
            }

            _dbContext.Expenses.Remove(expense);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Expense> GetExpenseById(int expenseId)
        {
            var expense = await _dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == expenseId);

            if (expense == null)
            {
                throw new InvalidOperationException("Expense with specified id doesn't exist.");
            }

            return expense;
        }

        public async Task Commit()
            => await _dbContext.SaveChangesAsync();

        public async Task<IEnumerable<ExpenseTotalAmountInCategoryDto>> GetTotalAmountInCategories(DateOnly startDate, DateOnly endDate)
        {
            var userId = _userContextService.GetCurrentUser().Id;

            var totalAmountInCategories = await _dbContext.Expenses
                .Include(e => e.Category)
                .Where(e => e.UserId == userId && (e.ExpenseDate >= startDate && e.ExpenseDate <= endDate))
                .GroupBy(e => e.Category.Name)
                .Select(g => new ExpenseTotalAmountInCategoryDto
                {
                    CategoryName = g.Key,
                    TotalAmount = g.Sum(c => c.Amount)
                })
                .ToListAsync();

            return totalAmountInCategories;
        }

        public decimal GetMoneySpent(int categoryId, DateOnly firstDayOfMonth, DateOnly lastDayOfMonth)
        {
            var userId = _userContextService.GetCurrentUser().Id;

            var moneySpent = _dbContext.Expenses
                .Where(e => e.UserId == userId && (e.ExpenseDate >= firstDayOfMonth && e.ExpenseDate <= lastDayOfMonth) &&
                    e.CategoryId == categoryId)
                .Sum(e => e.Amount);

            return moneySpent;
        }
    }
}
