using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Expense.Commands.SeedExpenseCategories
{
    public class SeedExpenseCategoriesToNewUserCommandHandler : IRequestHandler<SeedExpenseCategoriesToNewUserCommand>
    {
        private readonly IExpenseRepository _expenseRepository;

        public SeedExpenseCategoriesToNewUserCommandHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task Handle(SeedExpenseCategoriesToNewUserCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
            {
                throw new InvalidOperationException("User Id cannot be null or empty");
            }

            var expenseCategoriesAssignedToUserId = new List<ExpenseCategoryAssignedToUser>();
            var expenseCategoriesDefault = await _expenseRepository.GetDefaultCategories();

            foreach (var category in expenseCategoriesDefault)
            {
                expenseCategoriesAssignedToUserId.Add(
                    new ExpenseCategoryAssignedToUser()
                    {
                        UserId = request.UserId,
                        Name = category.Name
                    });
            }

            await _expenseRepository.SeedDefaultCategoriesToUser(expenseCategoriesAssignedToUserId);
        }
    }
}
