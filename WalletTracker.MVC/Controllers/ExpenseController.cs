using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WalletTracker.Application.Expense.Commands.CreateExpense;
using WalletTracker.Application.Expense.Queries.GetDefaultExpenseFormData;
using WalletTracker.Application.Expense.Queries.GetExpenseFormDataAfterValidation;
using WalletTracker.Application.Income.Queries.GetCategoriesAssignedToLoggedUse;
using WalletTracker.Application.Income.Queries.GetCategoriesAssignedToLoggedUser;
using WalletTracker.MVC.Extensions;
using WalletTracker.MVC.Models;

namespace WalletTracker.MVC.Controllers
{
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
            var command = await _mediator.Send(new GetDefaultExpenseFormDataQuery());
            return View(command);
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
    }
}
