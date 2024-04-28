using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WalletTracker.Application.Income;
using WalletTracker.Application.Income.Commands.CreateIncome;
using WalletTracker.Application.Income.Commands.DeleteIncomeById;
using WalletTracker.Application.Income.Commands.EditIncomeById;
using WalletTracker.Application.Income.Queries.EditIncomeById;
using WalletTracker.Application.Income.Queries.GetCategoriesAssignedToLoggedUse;
using WalletTracker.Application.Income.Queries.GetCategoriesAssignedToLoggedUser;
using WalletTracker.Application.Income.Queries.GetEditIncomeFormDataAfterValidation;
using WalletTracker.Domain.Entities;
using WalletTracker.MVC.Extensions;
using WalletTracker.MVC.Models;

namespace WalletTracker.MVC.Controllers
{
    [Authorize]
    public class IncomeController : Controller
    {
        private readonly IMediator _mediator;

        public IncomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var income = await _mediator.Send(new GetDefaultIncomeFormDataQuery());
            return View(income);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateIncomeCommand command)
        {
            if (!ModelState.IsValid)
            {
                var commandAfterValidation = await _mediator.Send(new GetIncomeFormDataAfterValidationQuery(command));

                return View(commandAfterValidation);
            }

            await _mediator.Send(command);

            this.SetNotification("success", "Income created");

            return RedirectToAction(nameof(Create));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var income = await _mediator.Send(new EditIncomeByIdQuery(id));

            return View(income);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditIncomeByIdCommand command)
        {
            if (!ModelState.IsValid)
            {
                var commandAfterValidation = await _mediator.Send(new GetEditIncomeFormDataAfterValidationQuery(command));

                return View(commandAfterValidation);
            }

            await _mediator.Send(command);

            this.SetNotification("success", "Income edited");

            return RedirectToAction("Index", "Balance");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteIncomeByIdCommand(id));

            this.SetNotification("warning", "Income deleted");

            return RedirectToAction("Index", "Balance");
        }
    }
}
