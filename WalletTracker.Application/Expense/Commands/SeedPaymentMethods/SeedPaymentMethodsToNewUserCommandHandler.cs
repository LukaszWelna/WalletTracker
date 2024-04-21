using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Expense.Commands.SeedPaymentMethods
{
    public class SeedPaymentMethodsToNewUserCommandHandler : IRequestHandler<SeedPaymentMethodsToNewUserCommand>
    {
        private readonly IExpenseRepository _expenseRepository;

        public SeedPaymentMethodsToNewUserCommandHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task Handle(SeedPaymentMethodsToNewUserCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
            {
                throw new InvalidOperationException("User Id cannot be null or empty");
            }

            var paymentMethodsAssignedToUserId = new List<PaymentMethodAssignedToUser>();
            var paymentMethodsDefault = await _expenseRepository.GetDefaultPaymentMethods();

            foreach (var method in paymentMethodsDefault)
            {
                paymentMethodsAssignedToUserId.Add(
                    new PaymentMethodAssignedToUser()
                    {
                        UserId = request.UserId,
                        Name = method.Name
                    });
            }

            await _expenseRepository.SeedDefaultPaymentMethodsToUser(paymentMethodsAssignedToUserId);
        }
    }
}
