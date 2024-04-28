using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Expense.Queries.GetExpensesFromPeriod;
using WalletTracker.Application.Expense.Queries.GetTotalAmountInCategories;
using WalletTracker.Application.Expense.Queries.GetTotalExpensesAmountFromPeriod;
using WalletTracker.Application.Income.Queries.GetIncomesFromPeriod;
using WalletTracker.Application.Income.Queries.GetTotalAmountFromPeriod;
using WalletTracker.Application.Income.Queries.GetTotalAmountInCategories;

namespace WalletTracker.Application.Balance.Queries.GetBalanceData
{
    public class GetBalanceDataQueryHandler : IRequestHandler<GetBalanceDataQuery, BalanceDto>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetBalanceDataQueryHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<BalanceDto> Handle(GetBalanceDataQuery request, CancellationToken cancellationToken)
        {
            var userIncomeDtos = await _mediator.Send(new GetUserIncomesFromPeriodQuery());

            var userExpenseDtos = await _mediator.Send(new GetUserExpensesFromPeriodQuery());

            var incomeTotalAmountInCategories = await _mediator.Send(new GetTotalIncomesAmountInCategoriesQuery());

            var expenseTotalAmountInCategories = await _mediator.Send(new GetTotalExpensesAmountInCategoriesQuery());

            var totalIncomesAmount = await _mediator.Send(new GetTotalIncomesAmountFromPeriodQuery());

            var totalExpensesAmount = await _mediator.Send(new GetTotalExpensesAmountFromPeriodQuery());

            var balanceCanvasDtos = _mapper.Map<List<BalanceCanvasDto>>(expenseTotalAmountInCategories);

            var balanceDto = new BalanceDto()
            {
                Incomes = userIncomeDtos,
                Expenses = userExpenseDtos,
                IncomeTotalAmountInCategories = incomeTotalAmountInCategories,
                ExpenseTotalAmountInCategories = expenseTotalAmountInCategories,
                TotalIncomesAmount = totalIncomesAmount,
                TotalExpensesAmount = totalExpensesAmount,
                BalanceCanvasDtos = balanceCanvasDtos
            };

            return balanceDto;
        }
    }
}
