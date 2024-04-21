using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletTracker.Application.Expense.Commands.SeedExpenseCategories
{
    public class SeedExpenseCategoriesToNewUserCommand : IRequest
    {
        public string UserId { get; set; }

        public SeedExpenseCategoriesToNewUserCommand(string userId)
        {
            UserId = userId;
        }
    }
}
