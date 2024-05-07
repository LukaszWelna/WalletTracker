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

        // Get all neccessary data to show incomes, expenses and balance in the view
        public async Task<BalanceDto> Handle(GetBalanceDataQuery request, CancellationToken cancellationToken)
        {
            // Incomes data in defined date range
            var userIncomes = await _incomeRepository.GetUserIncomesFromPeriod(request.StartDate, request.EndDate);

            var userIncomeDtos = _mapper.Map<List<List<GetIncomeDto>>>(userIncomes);

            // Expenses data in defined date range
            var userExpenses = await _expenseRepository.GetUserExpensesFromPeriod(request.StartDate, request.EndDate);

            var userExpenseDtos = _mapper.Map<List<List<GetExpenseDto>>>(userExpenses);

            // Get total income amount in categories in defined date range
            var incomeTotalAmountInCategories = await _incomeRepository.GetTotalAmountInCategories(request.StartDate, request.EndDate);

            // Get total expense amount in categories in defined date range
            var expenseTotalAmountInCategories = await _expenseRepository.GetTotalAmountInCategories(request.StartDate, request.EndDate);

            // Calculate total income amount in all categories in defined date range
            var totalIncomesAmount = userIncomeDtos.Sum(g => g.Sum(i => i.Amount));

            // Calculate total expense amount in all categories in defined date range
            var totalExpensesAmount = userExpenseDtos.Sum(g => g.Sum(e => e.Amount));

            // Get data to pie chart
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
