using MediatR;
using WalletTracker.Application.Expense.Commands.CreateExpense;

namespace WalletTracker.Application.Expense.Queries.GetDefaultExpenseFormData
{
    public class GetDefaultExpenseFormDataQuery : IRequest<CreateExpenseCommand>
    {

    }
}
