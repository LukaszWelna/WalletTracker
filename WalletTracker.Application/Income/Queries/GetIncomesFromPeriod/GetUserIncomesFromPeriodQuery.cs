using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletTracker.Application.Income.Queries.GetIncomesFromPeriod
{
    public class GetUserIncomesFromPeriodQuery : IRequest<IEnumerable<IEnumerable<GetIncomeDto>>>
    {

    }
}
