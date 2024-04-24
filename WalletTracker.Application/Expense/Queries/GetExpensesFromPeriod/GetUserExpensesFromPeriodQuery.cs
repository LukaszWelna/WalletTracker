using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletTracker.Application.Expense.Queries.GetExpensesFromPeriod
{
    public class GetUserExpensesFromPeriodQuery : IRequest<IEnumerable<IEnumerable<GetExpenseDto>>>
    {

    }
}
