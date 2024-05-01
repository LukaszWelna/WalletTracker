using MediatR;

namespace WalletTracker.Application.Expense.Commands.SeedExpenseCategories
{
    public class SeedExpenseCategoriesToNewUserCommand : IRequest
    {
        public string UserId { get; set; }

        public SeedExpenseCategoriesToNewUserCommand(string userId)
        {
            UserId = userId;
        }
    }
}
