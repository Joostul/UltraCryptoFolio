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
            var transactions = await _portfolioService.GetTransactions();
            var transactionViewModels = transactions.Select(t => t.ToViewModel());
            return View(transactionViewModels);
        }
        
        [Authorize(Policy = "RegisteredUser")]
        public async Task<IActionResult> CreateNewTransaction(TransactionViewModel transaction)
        {
            if (!ModelState.IsValid)
            {
                TempData["ModelState"] = ModelState;
            } else
            {
                var transactions = new List<Transaction>
                {
                    transaction.ToDomainModel()
                };
                await _portfolioService.AddTransactions(transactions.AsEnumerable());
                await _portfolioService.SavePortfolio();
            }
            return RedirectToAction("index");
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (TempData["ModelState"] != null && !ModelState.Equals(TempData["ModelState"]))
                ModelState.Merge(TempData["ModelState"] as ModelStateDictionary);

            base.OnActionExecuted(filterContext);
        }
    }
}