using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Expense.Commands.CreateExpense;

namespace WalletTracker.Application.Expense.Queries.GetDefaultExpenseFormData
{
    public class GetDefaultExpenseFormDataQuery : IRequest<CreateExpenseCommand>
    {

    }
}
