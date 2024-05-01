using MediatR;
using WalletTracker.Application.Expense.Commands.CreateExpense;

namespace WalletTracker.Application.Expense.Queries.GetExpenseFormDataAfterValidation
{
    public class GetExpenseFormDataAfterValidationQuery : IRequest<CreateExpenseCommand>
    {
        public CreateExpenseCommand CreateExpenseCommand { get; set; }

        public GetExpenseFormDataAfterValidationQuery(CreateExpenseCommand createExpenseCommand)
        {
            CreateExpenseCommand = createExpenseCommand;
        }
    }
}
