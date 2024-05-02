using MediatR;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Expense.Commands.SeedPaymentMethods
{
    public class SeedPaymentMethodsToNewUserCommandHandler : IRequestHandler<SeedPaymentMethodsToNewUserCommand>
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;

        public SeedPaymentMethodsToNewUserCommandHandler(IPaymentMethodRepository paymentMethodRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }

        public async Task Handle(SeedPaymentMethodsToNewUserCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
            {
                throw new InvalidOperationException("User Id cannot be null or empty");
            }

            var paymentMethodsAssignedToUserId = new List<PaymentMethodAssignedToUser>();

            var paymentMethodsDefault = await _paymentMethodRepository.GetDefaultPaymentMethods();

            foreach (var method in paymentMethodsDefault)
            {
                paymentMethodsAssignedToUserId.Add(
                    new PaymentMethodAssignedToUser()
                    {
                        UserId = request.UserId,
                        Name = method.Name
                    });
            }

            await _paymentMethodRepository.SeedDefaultPaymentMethodsToUser(paymentMethodsAssignedToUserId);
        }
    }
}
