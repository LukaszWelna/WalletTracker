using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Models;

namespace WalletTracker.Domain.Interfaces
{
    public interface IExpenseRepository
    {
        public Task Create(Expense expense);
        public Task<IEnumerable<ExpenseCategoryDefault>> GetDefaultCategories();
        public Task SeedDefaultCategoriesToUser(IEnumerable<ExpenseCategoryAssignedToUser> expenseCategoriesAssignedToUser);
        public Task<IEnumerable<PaymentMethodDefault>> GetDefaultPaymentMethods();
        public Task SeedDefaultPaymentMethodsToUser(IEnumerable<PaymentMethodAssignedToUser> paymentMethodsAssignedToUser);
        public Task<IEnumerable<ExpenseCategoryAssignedToUser>> GetCategoriesAssignedToLoggedUser();
        public Task<IEnumerable<PaymentMethodAssignedToUser>> GetPaymentMethodsAssignedToLoggedUser();
        public Task<IEnumerable<IEnumerable<Expense>>> GetUserExpensesFromPeriod();
        public Task DeleteExpenseById(int expenseId);
        public Task<Expense> GetExpenseById(int expenseId);
        public Task Commit();
        public Task<IEnumerable<ExpenseTotalAmountInCategoryDto>> GetTotalAmountInCategories();
    }
}
