using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Expense.Commands.EditExpenseById;
using WalletTracker.Application.Income.Commands.EditIncomeById;

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
