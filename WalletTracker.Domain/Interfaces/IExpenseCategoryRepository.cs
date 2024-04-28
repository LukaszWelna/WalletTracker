using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Entities;

namespace WalletTracker.Domain.Interfaces
{
    public interface IExpenseCategoryRepository
    {
        public Task<IEnumerable<ExpenseCategoryDefault>> GetDefaultCategories();
        public Task SeedDefaultCategoriesToUser(IEnumerable<ExpenseCategoryAssignedToUser> expenseCategoriesAssignedToUser);
        public Task<IEnumerable<PaymentMethodDefault>> GetDefaultPaymentMethods();
        public Task SeedDefaultPaymentMethodsToUser(IEnumerable<PaymentMethodAssignedToUser> paymentMethodsAssignedToUser);
        public Task<IEnumerable<ExpenseCategoryAssignedToUser>> GetCategoriesAssignedToLoggedUser();
        public Task<IEnumerable<PaymentMethodAssignedToUser>> GetPaymentMethodsAssignedToLoggedUser();
        public Task Create(ExpenseCategoryAssignedToUser category);
        public Task Delete(int categoryId);
        public Task Commit();
    }
}
