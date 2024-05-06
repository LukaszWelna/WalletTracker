using MediatR;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Expense.Commands.DeleteExpenseById
{
    public class DeleteExpenseByIdCommandHandler : IRequestHandler<DeleteExpenseByIdCommand>
    {
        private readonly IExpenseRepository _expenseRepository;

        public DeleteExpenseByIdCommandHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task Handle(DeleteExpenseByIdCommand request, CancellationToken cancellationToken)
        {
            if (request.ExpenseId <= 0)
            {
                throw new InvalidOperationException("Incorrect id value.");
            }

            await _expenseRepository.DeleteExpenseById(request.ExpenseId);
        }
    }
}
