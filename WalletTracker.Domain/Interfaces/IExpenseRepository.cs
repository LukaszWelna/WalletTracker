using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Entities;

namespace WalletTracker.Domain.Interfaces
{
    public interface IExpenseRepository
    {
        public Task<List<ExpenseCategoryDefault>> GetDefaultCategories();
        public Task SeedDefaultCategoriesToUser(List<ExpenseCategoryAssignedToUser> expenseCategoriesAssignedToUser);
        public Task<List<PaymentMethodDefault>> GetDefaultPaymentMethods();
        public Task SeedDefaultPaymentMethodsToUser(List<PaymentMethodAssignedToUser> paymentMethodsAssignedToUser);
    }
}
