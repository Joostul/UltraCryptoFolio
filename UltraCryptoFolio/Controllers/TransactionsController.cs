using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltraCryptoFolio.Extensions;
using UltraCryptoFolio.Models.DomainModels;
using UltraCryptoFolio.Models.ViewModels;
using UltraCryptoFolio.Services;

namespace UltraCryptoFolio.Controllers
{
    [Controller]
    public class TransactionsController : Controller
    {
        private IPortfolioService _portfolioService { get; set; }

        public TransactionsController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        [HttpGet]
        [Authorize(Policy = "RegisteredUser")]
        public async Task<IActionResult> Index()
        {
            return View((await _portfolioService.GetTransactions()).Select(t => t.ToViewModel()));
        }

        [HttpPost]
        [Authorize(Policy = "RegisteredUser")]
        public async Task<IActionResult> Index(TransactionViewModel transaction)
        {
            if (ModelState.IsValid)
            {
                var transactions = new List<Transaction>
                {
                    transaction.ToDomainModel()
                };
                await _portfolioService.AddTransactions(transactions.AsEnumerable());
                await _portfolioService.SavePortfolio();
                return RedirectToAction("index");
            }
            else
            {
                return View((await _portfolioService.GetTransactions()).Select(t => t.ToViewModel()));
            }
        }
    }
}