using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Income.Queries.GetTotalAmountFromPeriod
{
    public class GetTotalIncomesAmountFromPeriodQueryHandler : IRequestHandler<GetTotalIncomesAmountFromPeriodQuery, decimal>
    {
        private readonly IIncomeRepository _incomeRepository;

        public GetTotalIncomesAmountFromPeriodQueryHandler(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public Task<decimal> Handle(GetTotalIncomesAmountFromPeriodQuery request, CancellationToken cancellationToken)
            => Task.FromResult(_incomeRepository.GetTotalIncomesAmountFromPeriod());
    }
}
