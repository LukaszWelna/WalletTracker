using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Income.Commands.CreateIncome;

namespace WalletTracker.Application.Income.Queries.GetCategoriesAssignedToLoggedUse
{
    public class GetIncomeFormDataAfterValidationQuery : IRequest<CreateIncomeCommand>
    {
        public CreateIncomeCommand CreateIncomeCommand { get; set; }

        public GetIncomeFormDataAfterValidationQuery(CreateIncomeCommand createIncomeCommand)
        {
            CreateIncomeCommand = createIncomeCommand;
        }
    }
}
