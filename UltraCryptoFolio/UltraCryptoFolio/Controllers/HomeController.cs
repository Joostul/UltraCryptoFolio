using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UltraCryptoFolio.Models;

namespace UltraCryptoFolio.Controllers
{
    public class HomeController : Controller
    {
        private List<Transaction> _transactions = new List<Transaction>()
        {
            new Transaction()
                {
                    AmountReceived = 4000000000,
                    AmountSpent = 1000000000,
                    DateTime = DateTime.UtcNow,
                    ExchangeRate = 1,
                    Fee = 0,
                    ReveicingCurrency = Currency.BitcoinCash,
                    SpendingCurrency = Currency.Bitcoin
                },
            new Transaction()
                {
                    AmountReceived = 3130000000,
                    AmountSpent = 1000,
                    DateTime = DateTime.UtcNow,
                    ExchangeRate = 1,
                    Fee = 0,
                    ReveicingCurrency = Currency.BitcoinCash,
                    SpendingCurrency = Currency.Euro
                }
        };

        private Portfolio _currencyPortfolio = new Portfolio();

        public IActionResult Index()
        {
            _currencyPortfolio.BuildPortfolioFromTransacions(_transactions);
            _currencyPortfolio.CalculateMonetaryValue(Currency.Euro);

            return View(_currencyPortfolio);
        }
    }
}
