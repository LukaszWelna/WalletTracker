using MediatR;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Expense.Commands.SeedExpenseCategories
{
    public class SeedExpenseCategoriesToNewUserCommandHandler : IRequestHandler<SeedExpenseCategoriesToNewUserCommand>
    {
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;

        public SeedExpenseCategoriesToNewUserCommandHandler(IExpenseCategoryRepository expenseCategoryRepository)
        {
            _expenseCategoryRepository = expenseCategoryRepository;
        }

        public async Task Handle(SeedExpenseCategoriesToNewUserCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
            {
                throw new InvalidOperationException("User Id cannot be null or empty");
            }

            var expenseCategoriesAssignedToUserId = new List<ExpenseCategoryAssignedToUser>();
            var expenseCategoriesDefault = await _expenseCategoryRepository.GetDefaultCategories();

            foreach (var category in expenseCategoriesDefault)
            {
                expenseCategoriesAssignedToUserId.Add(
                    new ExpenseCategoryAssignedToUser()
                    {
                        UserId = request.UserId,
                        Name = category.Name
                    });
            }

            await _expenseCategoryRepository.SeedDefaultCategoriesToUser(expenseCategoriesAssignedToUserId);
        }
    }
}
