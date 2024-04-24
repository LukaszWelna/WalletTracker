using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WalletTracker.Application.Income;
using WalletTracker.Application.Income.Commands.CreateIncome;
using WalletTracker.Application.Income.Queries.GetCategoriesAssignedToLoggedUse;
using WalletTracker.Application.Income.Queries.GetCategoriesAssignedToLoggedUser;
using WalletTracker.Domain.Entities;
using WalletTracker.MVC.Extensions;
using WalletTracker.MVC.Models;

namespace WalletTracker.MVC.Controllers
{
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
    }
}
