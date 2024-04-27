using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using WalletTracker.Application.Income.Queries.GetCategoriesAssignedToLoggedUse;
using WalletTracker.Application.Settings.Commands.CreateIncomeCategory;
using WalletTracker.Application.Settings.Commands.DeleteIncomeCategory;
using WalletTracker.Application.Settings.Queries.GetIncomeCategoriesAssignedToLoggedUser;
using WalletTracker.Application.Settings.Queries.GetIncomeCategoryByName;
using WalletTracker.MVC.Extensions;

namespace WalletTracker.MVC.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly IMediator _mediator;

        public SettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult AddIncomeCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddIncomeCategory(CreateIncomeCategoryCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);

            this.SetNotification("success", "Income category created");

            return RedirectToAction(nameof(AddIncomeCategory));
        }

        [HttpPost]
        public async Task<IActionResult> CheckIncomeCategoryNameExists(string name)
        {
            var category = await _mediator.Send(new GetIncomeCategoryByNameQuery(name));

            if (category == null)
            {
                return Json(true);
            } else
            {
                return Json("Category name already exists in the database.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteIncomeCategory()
        {
            var incomeCategories = await _mediator.Send(new GetIncomeCategoriesAssignedToLoggedUserQuery());

            return View(incomeCategories);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteIncomeCategory(DeleteIncomeCategoryByIdCommand command)
        {
            if (!ModelState.IsValid)
            {
                command = await _mediator.Send(new GetIncomeCategoriesAssignedToLoggedUserQuery());

                return View(command);
            }

            await _mediator.Send(command);

            this.SetNotification("warning", "Income category deleted");

            return RedirectToAction(nameof(DeleteIncomeCategory));
        }


    }
}
