using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Income.Commands.CreateIncome;
using WalletTracker.Application.Income.Commands.EditIncomeById;

namespace WalletTracker.Application.Income.Queries.EditIncomeById
{
    public class EditIncomeByIdQuery : IRequest<EditIncomeByIdCommand>
    {
        public int IncomeId { get; set; }

        public EditIncomeByIdQuery(int incomeId)
        {
            IncomeId = incomeId;
        }
    }
}
