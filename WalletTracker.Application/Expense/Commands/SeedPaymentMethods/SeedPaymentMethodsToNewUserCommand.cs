using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletTracker.Application.Expense.Commands.SeedPaymentMethods
{
    public class SeedPaymentMethodsToNewUserCommand : IRequest
    {
        public string UserId { get; set; }

        public SeedPaymentMethodsToNewUserCommand(string userId)
        {
            UserId = userId;
        }
    }
}
