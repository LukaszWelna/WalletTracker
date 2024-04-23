﻿using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<ExpenseCategoryDefault>> GetDefaultCategories()
            => await _dbContext.ExpenseCategoriesDefault.ToListAsync();

        public async Task SeedDefaultCategoriesToUser(IEnumerable<ExpenseCategoryAssignedToUser> expenseCategoriesAssignedToUser)
        {
            _dbContext.ExpenseCategoriesAssignedToUsers.AddRange(expenseCategoriesAssignedToUser);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PaymentMethodDefault>> GetDefaultPaymentMethods()
            => await _dbContext.PaymentMethodsDefault.ToListAsync();

        public async Task SeedDefaultPaymentMethodsToUser(IEnumerable<PaymentMethodAssignedToUser> paymentMethodsAssignedToUser)
        {
            _dbContext.PaymentMethodsAssignedToUsers.AddRange(paymentMethodsAssignedToUser);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ExpenseCategoryAssignedToUser>> GetCategoriesAssignedToLoggedUser()
        {
            var userId = _userContextService.GetCurrentUser().Id;

            var categoriesAssignedToUser = await _dbContext
                .ExpenseCategoriesAssignedToUsers
                .Where(c => c.UserId == userId).ToListAsync();

            return categoriesAssignedToUser;
        }

        public async Task<IEnumerable<PaymentMethodAssignedToUser>> GetPaymentMethodsAssignedToLoggedUser()
        {
            var userId = _userContextService.GetCurrentUser().Id;

            var paymentMethodsAssignedToUser = await _dbContext
                .PaymentMethodsAssignedToUsers
                .Where(c => c.UserId == userId).ToListAsync();

            return paymentMethodsAssignedToUser;
        }

        public async Task Create(Expense expense)
        {
            _dbContext.Expenses.Add(expense);
            await _dbContext.SaveChangesAsync();
        }
    }
}
