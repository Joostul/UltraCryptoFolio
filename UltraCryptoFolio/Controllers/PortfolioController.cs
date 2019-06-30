using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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

        [Authorize(Policy = "RegisteredUser")]
        public async Task<IActionResult> Index()
        {
            _portfolioService.CreateExamplePortfolio();
            var totalWorth = await _portfolioService.GetTotalWorth();
            return View(totalWorth);
        }
    }
}