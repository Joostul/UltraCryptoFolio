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
                    AmountReceived = 110000000,
                    AmountSpent = 1000,
                    DateTime = DateTime.UtcNow,
                    ExchangeRate = 1,
                    Fee = 0,
                    ReveicingCurrency = Currency.Bitcoin,
                    SpendingCurrency = Currency.Euro
                },
            new Transaction()
                {
                    AmountReceived = 400000000,
                    AmountSpent = 100000000,
                    DateTime = DateTime.UtcNow,
                    ExchangeRate = 1,
                    Fee = 0,
                    ReveicingCurrency = Currency.BitcoinCash,
                    SpendingCurrency = Currency.Bitcoin
                },
            new Transaction()
                {
                    AmountReceived = 313000000,
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
