using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
