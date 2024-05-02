using MediatR;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Expense.Commands.EditExpenseById
{
    public class EditExpenseByIdCommandHandler : IRequestHandler<EditExpenseByIdCommand>
    {
        private readonly IExpenseRepository _expenseRepository;

        public EditExpenseByIdCommandHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task Handle(EditExpenseByIdCommand request, CancellationToken cancellationToken)
        {
            var expense = await _expenseRepository.GetExpenseById(request.Id);

            // Edit current data by values specified in the view
            expense.Amount = request.Amount;
            expense.ExpenseDate = request.ExpenseDate;
            expense.CategoryId = request.CategoryId;
            expense.PaymentId = request.PaymentId;
            expense.Comment = request.Comment;

            await _expenseRepository.Commit();
        }
    }
}
