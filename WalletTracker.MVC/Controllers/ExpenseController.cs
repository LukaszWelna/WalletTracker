using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WalletTracker.Application.Expense.Commands.CreateExpense;
using WalletTracker.Application.Expense.Commands.DeleteExpenseById;
using WalletTracker.Application.Expense.Commands.EditExpenseById;
using WalletTracker.Application.Expense.Queries.EditExpenseById;
using WalletTracker.Application.Expense.Queries.GetDefaultExpenseFormData;
using WalletTracker.Application.Expense.Queries.GetEditExpenseFormDataAfterValidationQuery;
using WalletTracker.Application.Expense.Queries.GetExpenseFormDataAfterValidation;
using WalletTracker.Application.Income.Queries.GetCategoriesAssignedToLoggedUse;
using WalletTracker.Application.Income.Queries.GetCategoriesAssignedToLoggedUser;
using WalletTracker.MVC.Extensions;
using WalletTracker.MVC.Models;

namespace WalletTracker.MVC.Controllers
{
    [Authorize]
    public class ExpenseController : Controller
    {
        private readonly IMediator _mediator;

        public ExpenseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var expense = await _mediator.Send(new GetDefaultExpenseFormDataQuery());
            return View(expense);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateExpenseCommand command)
        {
            if (!ModelState.IsValid)
            {
                var commandAfterValidation = await _mediator.Send(new GetExpenseFormDataAfterValidationQuery(command));

                return View(command);
            }

            await _mediator.Send(command);

            this.SetNotification("success", "Expense created");

            return RedirectToAction(nameof(Create));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var expense = await _mediator.Send(new EditExpenseByIdQuery(id));

            return View(expense);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditExpenseByIdCommand command)
        {
            if (!ModelState.IsValid)
            {
                var commandAfterValidation = await _mediator.Send(new GetEditExpenseFormDataAfterValidationQuery(command));

                return View(commandAfterValidation);
            }

            await _mediator.Send(command);

            this.SetNotification("success", "Expense edited");

            return RedirectToAction("Index", "Balance");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteExpenseByIdCommand(id));

            this.SetNotification("warning", "Expense deleted");

            return RedirectToAction("Index", "Balance");
        }
    }
}
