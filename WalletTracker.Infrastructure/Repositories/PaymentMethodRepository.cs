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
        {
            var paymentMethod = await _dbContext.PaymentMethodsAssignedToUsers
                .FirstAsync(p => p.Id == id);

            _dbContext.PaymentMethodsAssignedToUsers.Remove(paymentMethod);

            await _dbContext.SaveChangesAsync();
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
            => await _dbContext.PaymentMethodsAssignedToUsers
                .FirstAsync(p => p.Id == id);
    }
}
