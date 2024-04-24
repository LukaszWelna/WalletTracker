using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Income.Commands.CreateIncome;
using WalletTracker.Application.Income.Commands.EditIncomeById;

namespace WalletTracker.Application.Income.Queries.GetEditIncomeFormDataAfterValidation
{
    public class GetEditIncomeFormDataAfterValidationQuery : IRequest<EditIncomeByIdCommand>
    {
        public EditIncomeByIdCommand EditIncomeByIdCommand { get; set; }

        public GetEditIncomeFormDataAfterValidationQuery(EditIncomeByIdCommand editIncomeByIdCommand)
        {
            EditIncomeByIdCommand = editIncomeByIdCommand;
        }
    }
}
