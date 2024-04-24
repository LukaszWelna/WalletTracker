using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
