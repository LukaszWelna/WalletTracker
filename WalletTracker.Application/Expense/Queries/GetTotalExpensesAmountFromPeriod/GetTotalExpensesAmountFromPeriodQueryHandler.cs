using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Expense.Queries.GetTotalExpensesAmountFromPeriod
{
    public class GetTotalExpensesAmountFromPeriodQueryHandler : IRequestHandler<GetTotalExpensesAmountFromPeriodQuery, decimal>
    {
        private readonly IExpenseRepository _expenseRepository;

        public GetTotalExpensesAmountFromPeriodQueryHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public Task<decimal> Handle(GetTotalExpensesAmountFromPeriodQuery request, CancellationToken cancellationToken)
            => Task.FromResult(_expenseRepository.GetTotalIncomesAmountFromPeriod());
    }
}
