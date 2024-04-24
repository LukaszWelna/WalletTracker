using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletTracker.Application.Income.Commands.DeleteIncomeById
{
    public class DeleteIncomeByIdCommand : IRequest
    {
        public int IncomeId { get; set; }

        public DeleteIncomeByIdCommand(int incomeId)
        {
            IncomeId = incomeId;
        }
    }
}
