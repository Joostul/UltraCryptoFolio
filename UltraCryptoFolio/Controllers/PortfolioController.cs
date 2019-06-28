using Microsoft.AspNetCore.Mvc;
using UltraCryptoFolio.Services;

namespace UltraCryptoFolio.Controllers
{
    public class PortfolioController : Controller
    {
        private IPortfolioService _portfolioService { get; set; }

        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        public IActionResult Index()
        {
            decimal totalWorth = 0;
            if(HttpContext.User != null && HttpContext.User.Identity.IsAuthenticated)
            {
                totalWorth = _portfolioService.GetTotalWorth();
            }
            return View(totalWorth);
        }
    }
}