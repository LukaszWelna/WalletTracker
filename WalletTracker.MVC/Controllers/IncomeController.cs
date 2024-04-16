using Microsoft.AspNetCore.Mvc;
using WalletTracker.Application.Income;
using WalletTracker.Application.Services;
using WalletTracker.Domain.Entities;

namespace WalletTracker.MVC.Controllers
{
    public class IncomeController : Controller
    {
        private readonly IIncomeService _incomeService;

        public IncomeController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IncomeDto incomeDto)
        {
            await _incomeService.Create(incomeDto);
            return RedirectToAction(nameof(Create));
        }
    }
}
