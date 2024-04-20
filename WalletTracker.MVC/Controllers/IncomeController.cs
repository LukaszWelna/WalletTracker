using MediatR;
using Microsoft.AspNetCore.Mvc;
using WalletTracker.Application.Income;
using WalletTracker.Application.Income.Commands.CreateIncome;
using WalletTracker.Application.Income.Queries.GetCategoriesAssignedToLoggedUse;
using WalletTracker.Application.Income.Queries.GetCategoriesAssignedToLoggedUser;
using WalletTracker.Domain.Entities;

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
            var command = await _mediator.Send(new GetDefaultIncomeFormDataQuery());
            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateIncomeCommand command)
        {
            if (!ModelState.IsValid)
            {
                var commandAfterValidation = await _mediator.Send(new GetIncomeFormAfterValidationQuery(command));

                return View(commandAfterValidation);
            }

            await _mediator.Send(command);
            return RedirectToAction(nameof(Create));
        }
    }
}
