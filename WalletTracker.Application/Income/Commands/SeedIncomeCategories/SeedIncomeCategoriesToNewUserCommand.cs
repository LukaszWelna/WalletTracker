using MediatR;

namespace WalletTracker.Application.Income.Commands.SeedIncomeCategories
{
    public class SeedIncomeCategoriesToNewUserCommand : IRequest
    {
        public string UserId { get; set; }

        public SeedIncomeCategoriesToNewUserCommand(string userId)
        {
            UserId = userId;
        }
    }
}
