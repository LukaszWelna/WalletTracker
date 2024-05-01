using MediatR;
using WalletTracker.Application.Expense.Commands.EditExpenseById;

namespace WalletTracker.Application.Expense.Queries.GetEditExpenseFormDataAfterValidationQuery
{
    public class GetEditExpenseFormDataAfterValidationQuery : IRequest<EditExpenseByIdCommand>
    {
        public EditExpenseByIdCommand EditExpenseByIdCommand { get; set; }

        public GetEditExpenseFormDataAfterValidationQuery(EditExpenseByIdCommand editExpenseByIdCommand)
        {
            EditExpenseByIdCommand = editExpenseByIdCommand;
        }
    }
}
