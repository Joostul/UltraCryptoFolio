using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UltraCryptoFolio.Data;
using UltraCryptoFolio.Helpers;
using UltraCryptoFolio.Models;

namespace UltraCryptoFolio.Controllers
{
    public class HomeController : Controller
    {
        private List<Transaction> _transactions = ExampleTransactions.Transactions;

        public IActionResult Index()
        {
            var currencyPortfolio = new Portfolio(new PriceGetter(), _transactions);

            return View(currencyPortfolio);
        }
    }
}
