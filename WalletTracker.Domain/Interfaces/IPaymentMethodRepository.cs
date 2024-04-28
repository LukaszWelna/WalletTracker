using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Entities;

namespace WalletTracker.Domain.Interfaces
{
    public interface IPaymentMethodRepository
    {
        public Task<IEnumerable<PaymentMethodDefault>> GetDefaultPaymentMethods();
        public Task SeedDefaultPaymentMethodsToUser(IEnumerable<PaymentMethodAssignedToUser> paymentMethodsAssignedToUser);
        public Task<IEnumerable<PaymentMethodAssignedToUser>> GetPaymentMethodsAssignedToLoggedUser();
        public Task Create(PaymentMethodAssignedToUser category);
        public Task DeleteById(int id);
        public Task Commit();
        public Task<PaymentMethodAssignedToUser?> GetByName(string name);
    }
}
