using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using UltraCryptoFolio.Extensions;
using UltraCryptoFolio.Services;

namespace UltraCryptoFolio.Controllers
{
    public class TransactionsController : Controller
    {
        private IPortfolioService _portfolioService { get; set; }

        public TransactionsController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        [Authorize(Policy = "RegisteredUser")]
        public async Task<IActionResult> Index()
        {
            _portfolioService.CreateExamplePortfolio();
            var transactions = await _portfolioService.GetTransactions();
            var transactionViewModels = transactions.Select(t => t.ToViewModel());
            return View(transactionViewModels);
        }
    }
}