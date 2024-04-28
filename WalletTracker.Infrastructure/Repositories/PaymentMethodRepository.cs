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
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly WalletTrackerDbContext _dbContext;
        private readonly IUserContextService _userContextService;

        public PaymentMethodRepository(WalletTrackerDbContext dbContext, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
        }

        public async Task<IEnumerable<PaymentMethodDefault>> GetDefaultPaymentMethods()
            => await _dbContext.PaymentMethodsDefault.ToListAsync();

        public async Task SeedDefaultPaymentMethodsToUser(IEnumerable<PaymentMethodAssignedToUser> paymentMethodsAssignedToUser)
        {
            _dbContext.PaymentMethodsAssignedToUsers.AddRange(paymentMethodsAssignedToUser);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PaymentMethodAssignedToUser>> GetPaymentMethodsAssignedToLoggedUser()
        {
            var userId = _userContextService.GetCurrentUser().Id;

            var paymentMethodsAssignedToUser = await _dbContext
                .PaymentMethodsAssignedToUsers
                .Where(c => c.UserId == userId).ToListAsync();

            return paymentMethodsAssignedToUser;
        }

        public async Task Create(PaymentMethodAssignedToUser paymentMethod)
        {
            _dbContext.PaymentMethodsAssignedToUsers.Add(paymentMethod);

            await _dbContext.SaveChangesAsync();
        }

        public Task DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Commit()
        {
            throw new NotImplementedException();
        }

        public async Task<PaymentMethodAssignedToUser?> GetByName(string name)
        {
            var userId = _userContextService.GetCurrentUser().Id;

            var paymentMethod = await _dbContext.PaymentMethodsAssignedToUsers
                .Where(c => c.UserId == userId)
                .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());

            return paymentMethod;
        }
    }
}
