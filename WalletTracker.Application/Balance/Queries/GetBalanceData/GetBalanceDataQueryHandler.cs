using AutoMapper;
using MediatR;
using WalletTracker.Application.Expense;
using WalletTracker.Application.Income;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Balance.Queries.GetBalanceData
{
    public class GetBalanceDataQueryHandler : IRequestHandler<GetBalanceDataQuery, BalanceDto>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;

        public GetBalanceDataQueryHandler(IIncomeRepository incomeRepository, IExpenseRepository expenseRepository, IMapper mapper)
        {
            _incomeRepository = incomeRepository;
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        public async Task<BalanceDto> Handle(GetBalanceDataQuery request, CancellationToken cancellationToken)
        {
            var userIncomes = await _incomeRepository.GetUserIncomesFromPeriod(request.StartDate, request.EndDate);

            var userIncomeDtos = _mapper.Map<List<List<GetIncomeDto>>>(userIncomes);

            var userExpenses = await _expenseRepository.GetUserExpensesFromPeriod(request.StartDate, request.EndDate);

            var userExpenseDtos = _mapper.Map<List<List<GetExpenseDto>>>(userExpenses);

            var incomeTotalAmountInCategories = await _incomeRepository.GetTotalAmountInCategories(request.StartDate, request.EndDate);

            var expenseTotalAmountInCategories = await _expenseRepository.GetTotalAmountInCategories(request.StartDate, request.EndDate);

            var totalIncomesAmount = userIncomeDtos.Sum(g => g.Sum(i => i.Amount));

            var totalExpensesAmount = userExpenseDtos.Sum(g => g.Sum(e => e.Amount));

            var balanceCanvasDtos = _mapper.Map<List<BalanceCanvasDto>>(expenseTotalAmountInCategories);

            var balanceDto = new BalanceDto()
            {
                Incomes = userIncomeDtos,
                Expenses = userExpenseDtos,
                IncomeTotalAmountInCategories = incomeTotalAmountInCategories,
                ExpenseTotalAmountInCategories = expenseTotalAmountInCategories,
                TotalIncomesAmount = totalIncomesAmount,
                TotalExpensesAmount = totalExpensesAmount,
                BalanceCanvasDtos = balanceCanvasDtos,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            return balanceDto;
        }
    }
}
