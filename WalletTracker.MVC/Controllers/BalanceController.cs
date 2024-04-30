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
        public async Task<IActionResult> Index(GetBalanceDataQuery query)
        {
            var balanceDto = await _mediator.Send(query);

            return View(balanceDto);
        }
    }
}
