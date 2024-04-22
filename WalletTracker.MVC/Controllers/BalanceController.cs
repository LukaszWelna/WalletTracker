using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            return View();
        }


    }
}
