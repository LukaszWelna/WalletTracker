using MediatR;
using Microsoft.AspNetCore.Mvc;
using WalletTracker.Application.Expense.Commands.DeleteExpenseById;
using WalletTracker.Application.Expense.Commands.EditExpenseById;
using WalletTracker.Application.Expense.Queries.EditExpenseById;
using WalletTracker.Application.Expense.Queries.GetEditExpenseFormDataAfterValidationQuery;
using WalletTracker.Application.Expense.Queries.GetExpensesFromPeriod;
using WalletTracker.Application.Income.Commands.DeleteIncomeById;
using WalletTracker.Application.Income.Commands.EditIncomeById;
using WalletTracker.Application.Income.Queries.EditIncomeById;
using WalletTracker.Application.Income.Queries.GetEditIncomeFormDataAfterValidation;
using WalletTracker.Application.Income.Queries.GetIncomesFromPeriod;
using WalletTracker.MVC.Extensions;
using WalletTracker.MVC.Models;

namespace WalletTracker.MVC.Controllers
{
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

            var balanceModel = new BalanceModel()
            {
                Incomes = userIncomeDtos,
                Expenses = userExpenseDtos
            };

            return View(balanceModel);
        }

        [HttpPost]
        public async Task<IActionResult> IncomeDelete(int id)
        {
            await _mediator.Send(new DeleteIncomeByIdCommand(id));

            this.SetNotification("warning", "Income deleted");

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ExpenseDelete(int id)
        {
            await _mediator.Send(new DeleteExpenseByIdCommand(id));

            this.SetNotification("warning", "Expense deleted");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> IncomeEdit(int id)
        {
            var income = await _mediator.Send(new EditIncomeByIdQuery(id));

            return View(income);
        }

        [HttpPost]
        public async Task<IActionResult> IncomeEdit(int id, EditIncomeByIdCommand command)
        {
            if (!ModelState.IsValid)
            {
                var commandAfterValidation = await _mediator.Send(new GetEditIncomeFormDataAfterValidationQuery(command));

                return View(commandAfterValidation);
            }

            await _mediator.Send(command);

            this.SetNotification("success", "Income edited");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ExpenseEdit(int id)
        {
            var expense = await _mediator.Send(new EditExpenseByIdQuery(id));

            return View(expense);
        }

        [HttpPost]
        public async Task<IActionResult> ExpenseEdit(int id, EditExpenseByIdCommand command)
        {
            if (!ModelState.IsValid)
            {
                var commandAfterValidation = await _mediator.Send(new GetEditExpenseFormDataAfterValidationQuery(command));

                return View(commandAfterValidation);
            }

            await _mediator.Send(command);

            this.SetNotification("success", "Expense edited");

            return RedirectToAction(nameof(Index));
        }

    }
}
