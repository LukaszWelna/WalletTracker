using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Expense.Queries.GetMoneySpentData
{
    public class GetMoneySpentDataQueryHandler : IRequestHandler<GetMoneySpentDataQuery, decimal>
    {
        private readonly IExpenseRepository _expenseRepository;

        public GetMoneySpentDataQueryHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public Task<decimal> Handle(GetMoneySpentDataQuery request, CancellationToken cancellationToken)
        {
            var firstDayOfMonth = DateOnly.FromDateTime(new DateTime(request.Date.Year, request.Date.Month, 1));
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var moneySpent = _expenseRepository.GetMoneySpent(request.CategoryId, firstDayOfMonth, lastDayOfMonth);

            return Task.FromResult(moneySpent);
        }
    }
}
