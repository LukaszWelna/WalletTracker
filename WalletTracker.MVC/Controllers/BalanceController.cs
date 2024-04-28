using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WalletTracker.Application.Balance.Queries.GetBalanceData;

namespace WalletTracker.MVC.Controllers
{
    [Authorize]
    public class BalanceController : Controller
    {
        private readonly IMediator _mediator;

        public BalanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var balanceDto = await _mediator.Send(new GetBalanceDataQuery());

            return View(balanceDto);
        }
    }
}
