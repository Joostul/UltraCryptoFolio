using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UltraCryptoFolio.Models;

namespace UltraCryptoFolio.Controllers
{
    public class HomeController : Controller
    {
        private List<Transaction> _transactions = new List<Transaction>()
        {
            new Investment()
                {
                    AmountReceived = 110000000,
                    AmountSpent = 1000.10m,
                    DateTime = DateTime.UtcNow,
                    ExchangeRate = 1,
                    Fee = 0,
                    ReveicingCurrency = CryptoCurrency.Bitcoin,
                    SpendingCurrency = Currency.Euro
                },
            new Trade()
                {
                    AmountReceived = 400000000,
                    AmountSpent = 100000000,
                    DateTime = DateTime.UtcNow,
                    ExchangeRate = 1,
                    Fee = 0,
                    ReveicingCurrency = CryptoCurrency.BitcoinCash,
                    SpendingCurrency = CryptoCurrency.Bitcoin
                },
            new Investment()
                {
                    AmountReceived = 313000000,
                    AmountSpent = 1000.00m,
                    DateTime = DateTime.UtcNow,
                    ExchangeRate = 1,
                    Fee = 0,
                    ReveicingCurrency = CryptoCurrency.BitcoinCash,
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
