using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WalletTracker.Application.Expense.Commands.CreateExpense;
using WalletTracker.Application.Expense.Commands.DeleteExpenseById;
using WalletTracker.Application.Expense.Commands.EditExpenseById;
using WalletTracker.Application.Expense.Queries.EditExpenseById;
using WalletTracker.Application.Expense.Queries.GetCategoryLimitData;
using WalletTracker.Application.Expense.Queries.GetDefaultExpenseFormData;
using WalletTracker.Application.Expense.Queries.GetEditExpenseFormDataAfterValidationQuery;
using WalletTracker.Application.Expense.Queries.GetExpenseFormDataAfterValidation;
using WalletTracker.Application.Expense.Queries.GetMoneySpentData;
using WalletTracker.MVC.Extensions;

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

        // Manage creating of new expense
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

        // Manage editing of the expense
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var expense = await _mediator.Send(new EditExpenseByIdQuery(id));

            return View(expense);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditExpenseByIdCommand command, string startDate, string endDate)
        {
            if (!ModelState.IsValid)
            {
                var commandAfterValidation = await _mediator.Send(new GetEditExpenseFormDataAfterValidationQuery(command));

                return View(commandAfterValidation);
            }

            await _mediator.Send(command);

            this.SetNotification("success", "Expense edited");

            return RedirectToAction("Index", "Balance", new { startDate, endDate });
        }

        // Manage deleting of the expense
        [HttpPost]
        public async Task<IActionResult> Delete(int id, string startDate, string endDate)
        {
            await _mediator.Send(new DeleteExpenseByIdCommand(id));

            this.SetNotification("warning", "Expense deleted");

            return RedirectToAction("Index", "Balance", new { startDate, endDate });
        }

        // Get limit of defined category
        [HttpGet]
        public async Task<IActionResult> GetCategoryLimit(int id)
        {
            var data = await _mediator.Send(new GetCategoryLimitDataQuery(id));

            return Ok(data);
        }

        // Get money left in defined month based on selected expense category
        [HttpGet]
        [Route("Expense/GetMoneySpent/{categoryId}/{date}")]
        public async Task<IActionResult> GetMoneySpent(int categoryId, DateOnly date)
        {
            var data = await _mediator.Send(new GetMoneySpentDataQuery(categoryId, date));

            return Ok(data);
        }
    }
}
