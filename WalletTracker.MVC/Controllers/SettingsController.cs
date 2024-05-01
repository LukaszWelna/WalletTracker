using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WalletTracker.Application.Settings.Commands.CreateExpenseCategory;
using WalletTracker.Application.Settings.Commands.CreateIncomeCategory;
using WalletTracker.Application.Settings.Commands.CreatePaymentMethod;
using WalletTracker.Application.Settings.Commands.DeleteExpenseCategoryById;
using WalletTracker.Application.Settings.Commands.DeleteIncomeCategory;
using WalletTracker.Application.Settings.Commands.DeletePaymentMethodById;
using WalletTracker.Application.Settings.Commands.EditExpenseCategoryById;
using WalletTracker.Application.Settings.Commands.EditIncomeCategoryById;
using WalletTracker.Application.Settings.Commands.EditPaymentMethodById;
using WalletTracker.Application.Settings.Queries.GetExpenseCategoryById;
using WalletTracker.Application.Settings.Queries.GetExpenseCategoryByName;
using WalletTracker.Application.Settings.Queries.GetExpenseCategoryFormToDelete;
using WalletTracker.Application.Settings.Queries.GetExpenseCategoryFormToEdit;
using WalletTracker.Application.Settings.Queries.GetIncomeCategoriesAssignedToLoggedUser;
using WalletTracker.Application.Settings.Queries.GetIncomeCategoryByName;
using WalletTracker.Application.Settings.Queries.GetIncomeCategoryFormToEdit;
using WalletTracker.Application.Settings.Queries.GetPaymentMethodByName;
using WalletTracker.Application.Settings.Queries.GetPaymentMethodFormToDelete;
using WalletTracker.Application.Settings.Queries.GetPaymentMethodFormToEdit;
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

        [HttpPost]
        public async Task<IActionResult> CheckIncomeCategoryNameExists(int id, string name)
        {
            var category = await _mediator.Send(new GetIncomeCategoryByNameQuery(name));

            if (category != null && category.Id != id)
            {
                return Json("Category name already exists in the database.");
            }
            else
            {
                return Json(true);
            }
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

        [HttpGet]
        public async Task<IActionResult> EditIncomeCategory()
        {
            var incomeCategories = await _mediator.Send(new GetIncomeCategoryFormToEditQuery());

            return View(incomeCategories);
        }

        [HttpPost]
        public async Task<IActionResult> EditIncomeCategory(EditIncomeCategoryByIdCommand command)
        {
            if (!ModelState.IsValid)
            {
                command = await _mediator.Send(new GetIncomeCategoryFormToEditQuery());

                return View(command);
            }

            await _mediator.Send(command);

            this.SetNotification("success", "Income category edited");

            return RedirectToAction(nameof(EditIncomeCategory));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteIncomeCategory()
        {
            var incomeCategories = await _mediator.Send(new GetIncomeCategoryFormToDeleteQuery());

            return View(incomeCategories);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteIncomeCategory(DeleteIncomeCategoryByIdCommand command)
        {
            if (!ModelState.IsValid)
            {
                command = await _mediator.Send(new GetIncomeCategoryFormToDeleteQuery());

                return View(command);
            }

            await _mediator.Send(command);

            this.SetNotification("warning", "Income category deleted");

            return RedirectToAction(nameof(DeleteIncomeCategory));
        }

        [HttpPost]
        public async Task<IActionResult> CheckExpenseCategoryNameExists(int id, string name)
        {
            var category = await _mediator.Send(new GetExpenseCategoryByNameQuery(name));

            if (category != null && category.Id != id)
            {
                return Json("Category name already exists in the database.");
            }
            else
            {
                return Json(true);
            }
        }

        [HttpGet]
        public IActionResult AddExpenseCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddExpenseCategory(CreateExpenseCategoryCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);

            this.SetNotification("success", "Expense category created");

            return RedirectToAction(nameof(AddExpenseCategory));
        }

        [HttpGet]
        public async Task<IActionResult> EditExpenseCategory()
        {
            var expenseCategories = await _mediator.Send(new GetExpenseCategoryFormToEditQuery());

            return View(expenseCategories);
        }

        [HttpPost]
        public async Task<IActionResult> EditExpenseCategory(EditExpenseCategoryByIdCommand command)
        {
            if (!ModelState.IsValid)
            {
                command = await _mediator.Send(new GetExpenseCategoryFormToEditQuery());

                return View(command);
            }

            await _mediator.Send(command);

            this.SetNotification("success", "Expense category edited");

            return RedirectToAction(nameof(EditExpenseCategory));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteExpenseCategory()
        {
            var incomeCategories = await _mediator.Send(new GetExpenseCategoryFormToDeleteQuery());

            return View(incomeCategories);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteExpenseCategory(DeleteExpenseCategoryByIdCommand command)
        {
            if (!ModelState.IsValid)
            {
                command = await _mediator.Send(new GetExpenseCategoryFormToDeleteQuery());

                return View(command);
            }

            await _mediator.Send(command);

            this.SetNotification("warning", "Expense category deleted");

            return RedirectToAction(nameof(DeleteExpenseCategory));
        }

        [HttpGet]
        public async Task<IActionResult> GetExpenseCategoryById(int id)
        {
            var data = await _mediator.Send(new GetExpenseCategoryByIdQuery(id));

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CheckPaymentMethodNameExists(int id, string name)
        {
            var paymentMethod = await _mediator.Send(new GetPaymentMethodByNameQuery(name));

            if (paymentMethod != null && paymentMethod.Id != id)
            {
                return Json("Payment method name already exists in the database.");
            }
            else
            {
                return Json(true);
            }
        }

        [HttpGet]
        public IActionResult AddPaymentMethod()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPaymentMethod(CreatePaymentMethodCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);

            this.SetNotification("success", "Payment method created");

            return RedirectToAction(nameof(AddPaymentMethod));
        }

        [HttpGet]
        public async Task<IActionResult> EditPaymentMethod()
        {
            var paymentMethods = await _mediator.Send(new GetPaymentMethodFormToEditQuery());

            return View(paymentMethods);
        }

        [HttpPost]
        public async Task<IActionResult> EditPaymentMethod(EditPaymentMethodByIdCommand command)
        {
            if (!ModelState.IsValid)
            {
                command = await _mediator.Send(new GetPaymentMethodFormToEditQuery());

                return View(command);
            }

            await _mediator.Send(command);

            this.SetNotification("success", "Payment method edited");

            return RedirectToAction(nameof(EditPaymentMethod));
        }

        [HttpGet]
        public async Task<IActionResult> DeletePaymentMethod()
        {
            var paymentMethods = await _mediator.Send(new GetPaymentMethodFormToDeleteQuery());

            return View(paymentMethods);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePaymentMethod(DeletePaymentMethodByIdCommand command)
        {
            if (!ModelState.IsValid)
            {
                command = await _mediator.Send(new GetPaymentMethodFormToDeleteQuery());

                return View(command);
            }

            await _mediator.Send(command);

            this.SetNotification("warning", "Payment method deleted");

            return RedirectToAction(nameof(DeletePaymentMethod));
        }
    }
}
