using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UltraCryptoFolio.Data;
using UltraCryptoFolio.Models;

namespace UltraCryptoFolio.Controllers
{
    public class HomeController : Controller
    {
        private List<Transaction> _transactions = ExampleTransactions.Transactions;

        private Portfolio _currencyPortfolio = new Portfolio();

        public IActionResult Index()
        {
            _currencyPortfolio.BuildPortfolioFromTransacions(_transactions);
            _currencyPortfolio.CalculateMonetaryValue(Currency.Euro);

            return View(_currencyPortfolio);
        }
    }
}
