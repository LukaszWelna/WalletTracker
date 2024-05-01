using MediatR;

namespace WalletTracker.Application.Expense.Commands.SeedPaymentMethods
{
    public class SeedPaymentMethodsToNewUserCommand : IRequest
    {
        public string UserId { get; set; }

        public SeedPaymentMethodsToNewUserCommand(string userId)
        {
            UserId = userId;
        }
    }
}
