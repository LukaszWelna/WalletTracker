using MediatR;

namespace WalletTracker.Application.Expense.Commands.DeleteExpenseById
{
    public class DeleteExpenseByIdCommand : IRequest
    {
        public int ExpenseId { get; set; }

        public DeleteExpenseByIdCommand(int expenseId)
        {
            ExpenseId = expenseId;
        }
    }
}
