using MediatR;
using Microsoft.AspNetCore.Mvc;
using WalletTracker.Application.Income.Queries.GetIncomesFromPeriod;

namespace WalletTracker.MVC.Controllers
{
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
            var incomeDtos = await _mediator.Send(new GetUserIncomesFromPeriodQuery());

            return View(incomeDtos);
        }


    }
}
