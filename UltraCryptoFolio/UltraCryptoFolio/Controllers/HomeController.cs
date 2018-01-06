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

        private Portfolio _currencyPortfolio;

        public IActionResult Index()
        {
            _currencyPortfolio = new Portfolio(new PriceGetter(), _transactions);

            return View(_currencyPortfolio);
        }
    }
}
