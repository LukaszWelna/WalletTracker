using Microsoft.EntityFrameworkCore;
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

            var paymentMethodsAssignedToUser = await _dbContext.PaymentMethodsAssignedToUsers
                .Where(p => p.UserId == userId).ToListAsync();

            return paymentMethodsAssignedToUser;
        }

        public async Task Create(PaymentMethodAssignedToUser paymentMethod)
        {
            _dbContext.PaymentMethodsAssignedToUsers.Add(paymentMethod);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        // Use transaction to perform 2 operations on the database
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var expenses = await _dbContext.Expenses
                        .Where(e => e.PaymentId == id)
                        .ToListAsync();

                    if (expenses != null && expenses.Count > 0)
                    {
                        _dbContext.Expenses.RemoveRange(expenses);
                    }

                    var paymentMethod = await _dbContext.PaymentMethodsAssignedToUsers
                        .FirstOrDefaultAsync(p => p.Id == id);

                    if (paymentMethod == null)
                    {
                        throw new InvalidOperationException("Payment method with specified id doesn't exist.");
                    }

                    _dbContext.PaymentMethodsAssignedToUsers.Remove(paymentMethod);

                    await _dbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    transaction.Rollback();

                    throw new InvalidOperationException("An Error occured while deleting the payment method.");
                }
            }  
        }

        public async Task Commit()
            => await _dbContext.SaveChangesAsync();

        public async Task<PaymentMethodAssignedToUser?> GetByName(string name)
        {
            var userId = _userContextService.GetCurrentUser().Id;

            var paymentMethod = await _dbContext.PaymentMethodsAssignedToUsers
                .Where(p => p.UserId == userId)
                .FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower());

            return paymentMethod;
        }

        public async Task<PaymentMethodAssignedToUser> GetById(int id)
        {
            var paymentMethod = await _dbContext.PaymentMethodsAssignedToUsers
                .FirstOrDefaultAsync(p => p.Id == id);

            if (paymentMethod == null)
            {
                throw new InvalidOperationException("Payment method with specified id doesn't exist.");
            }

            return paymentMethod;
        }
    }
}
