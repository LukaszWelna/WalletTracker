using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Expense.Commands.CreateExpense;
using WalletTracker.Application.Income.Commands.CreateIncome;

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
