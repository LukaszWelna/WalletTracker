using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Income.Commands.CreateIncome;

namespace WalletTracker.Application.Income.Queries.GetCategoriesAssignedToLoggedUse
{
    public class GetIncomeFormAfterValidationQuery : IRequest<CreateIncomeCommand>
    {
        public CreateIncomeCommand CreateIncomeCommand { get; set; }

        public GetIncomeFormAfterValidationQuery(CreateIncomeCommand createIncomeCommand)
        {
            CreateIncomeCommand = createIncomeCommand;
        }
    }
}
