using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UltraCryptoFolio.Models.ViewModels;
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
            var portfolioViewModel = new PortfolioViewModel();

            _portfolioService.CreateExamplePortfolio();
            portfolioViewModel.TotalWorth = await _portfolioService.GetTotalWorth();
            portfolioViewModel.TotalInvested = await _portfolioService.GetTotalInvested();
            return View(portfolioViewModel);
        }
    }
}