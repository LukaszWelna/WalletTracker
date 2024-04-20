using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletTracker.Application.Income.Commands.SeedIncomeCategories
{
    public class SeedIncomeCategoriesToNewUserCommand : IRequest
    {
        public string UserId { get; set; }

        public SeedIncomeCategoriesToNewUserCommand(string userId)
        {
            UserId = userId;
        }
    }
}
