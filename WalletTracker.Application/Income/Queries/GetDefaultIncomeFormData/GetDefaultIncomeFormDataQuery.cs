using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Income.Commands.CreateIncome;

namespace WalletTracker.Application.Income.Queries.GetCategoriesAssignedToLoggedUser
{
    public class GetDefaultIncomeFormDataQuery : IRequest<CreateIncomeCommand>
    {

    }
}
