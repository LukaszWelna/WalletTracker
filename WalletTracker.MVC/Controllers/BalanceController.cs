using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WalletTracker.Application.Balance.Queries.GetBalanceData;
using WalletTracker.Application.Expense.Commands.DeleteExpenseById;
using WalletTracker.Application.Expense.Commands.EditExpenseById;
using WalletTracker.Application.Expense.Queries.EditExpenseById;
using WalletTracker.Application.Expense.Queries.GetEditExpenseFormDataAfterValidationQuery;
using WalletTracker.Application.Expense.Queries.GetExpensesFromPeriod;
using WalletTracker.Application.Expense.Queries.GetTotalAmountInCategories;
using WalletTracker.Application.Expense.Queries.GetTotalExpensesAmountFromPeriod;
using WalletTracker.Application.Income.Commands.DeleteIncomeById;
using WalletTracker.Application.Income.Commands.EditIncomeById;
using WalletTracker.Application.Income.Queries.EditIncomeById;
using WalletTracker.Application.Income.Queries.GetEditIncomeFormDataAfterValidation;
using WalletTracker.Application.Income.Queries.GetIncomesFromPeriod;
using WalletTracker.Application.Income.Queries.GetTotalAmountFromPeriod;
using WalletTracker.Application.Income.Queries.GetTotalAmountInCategories;
using WalletTracker.MVC.Extensions;
using WalletTracker.MVC.Models;

namespace WalletTracker.MVC.Controllers
{
    [Authorize]
    public class BalanceController : Controller
    {
        private readonly IMediator _mediator;

        public BalanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userIncomeDtos = await _mediator.Send(new GetUserIncomesFromPeriodQuery());

            var userExpenseDtos = await _mediator.Send(new GetUserExpensesFromPeriodQuery());

            var incomeTotalAmountInCategories = await _mediator.Send(new GetTotalIncomesAmountInCategoriesQuery());

            var expenseTotalAmountInCategories = await _mediator.Send(new GetTotalExpensesAmountInCategoriesQuery());

            var totalIncomesAmount = await _mediator.Send(new GetTotalIncomesAmountFromPeriodQuery());

            var totalExpensesAmount = await _mediator.Send(new GetTotalExpensesAmountFromPeriodQuery());

            var balanceDtos = await _mediator.Send(new GetBalanceDataQuery(expenseTotalAmountInCategories));
            
            var balanceModel = new BalanceModel()
            {
                Incomes = userIncomeDtos,
                Expenses = userExpenseDtos,
                IncomeTotalAmountInCategories = incomeTotalAmountInCategories,
                ExpenseTotalAmountInCategories = expenseTotalAmountInCategories,
                TotalIncomesAmount = totalIncomesAmount,
                TotalExpensesAmount = totalExpensesAmount,
                BalanceDtos = balanceDtos
            };

            return View(balanceModel);
        }
    }
}
