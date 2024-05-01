using MediatR;
using WalletTracker.Application.Expense.Commands.EditExpenseById;

namespace WalletTracker.Application.Expense.Queries.EditExpenseById
{
    public class EditExpenseByIdQuery : IRequest<EditExpenseByIdCommand>
    {
        public int ExpenseId { get; set; }

        public EditExpenseByIdQuery(int expenseId)
        {
            ExpenseId = expenseId;
        }
    }
}
