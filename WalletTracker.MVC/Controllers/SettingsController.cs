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

        // Check if defined income category name already exists in the database
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

        // Manage adding a new income category
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

        // Manage editing an income category
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

        // Manage deleting an income category
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

        // Check if defined expense category name already exists in the database
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

        // Manage adding a new expense category
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

        // Manage editing a expense category
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

        // Manage deleting a expense category
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

        // Get expense category by defined id 
        [HttpGet]
        public async Task<IActionResult> GetExpenseCategoryById(int id)
        {
            var data = await _mediator.Send(new GetExpenseCategoryByIdQuery(id));

            return Ok(data);
        }

        // Check if defined payment method name already exists in the database
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

        // Manage adding a new payment method
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

        // Manage editing a payment method
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

        // Manage deleting a payment method
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
