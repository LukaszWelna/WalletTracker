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
        public async Task<List<ExpenseCategoryDefault>> GetDefaultCategories()
            => await _dbContext.ExpenseCategoriesDefault.ToListAsync();

        public async Task SeedDefaultCategoriesToUser(List<ExpenseCategoryAssignedToUser> expenseCategoriesAssignedToUser)
        {
            _dbContext.ExpenseCategoriesAssignedToUsers.AddRange(expenseCategoriesAssignedToUser);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<PaymentMethodDefault>> GetDefaultPaymentMethods()
            => await _dbContext.PaymentMethodsDefault.ToListAsync();

        public async Task SeedDefaultPaymentMethodsToUser(List<PaymentMethodAssignedToUser> paymentMethodsAssignedToUser)
        {
            _dbContext.PaymentMethodsAssignedToUsers.AddRange(paymentMethodsAssignedToUser);
            await _dbContext.SaveChangesAsync();
        }
    }
}
