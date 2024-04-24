using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Income;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Expense.Queries.GetExpensesFromPeriod
{
    public class GetUserExpensesFromPeriodQueryHandler : IRequestHandler<GetUserExpensesFromPeriodQuery, IEnumerable<IEnumerable<GetExpenseDto>>>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;

        public GetUserExpensesFromPeriodQueryHandler(IExpenseRepository expenseRepository, IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IEnumerable<GetExpenseDto>>> Handle(GetUserExpensesFromPeriodQuery request, CancellationToken cancellationToken)
        {
            var expenses = await _expenseRepository.GetUserExpensesFromPeriod();

            var expenseDtos = _mapper.Map<List<List<GetExpenseDto>>>(expenses);

            return expenseDtos;
        }
    }
}
